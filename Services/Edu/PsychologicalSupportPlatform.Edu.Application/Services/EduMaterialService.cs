using MapsterMapper;
using Nest;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.Services;

public class EduMaterialService: IEduMaterialService
{
    private readonly IEduMaterialRepository _eduMaterialRepository;
    private readonly IUserGrpcClient _userGrpcClient;
    private readonly IMapper _mapper;
    private readonly IMinioRepository _minioRepository;
    private readonly IStudentHasEduMaterialRepository _studentHasEduMaterialRepository;
    private readonly IElasticClient _elasticClient;
    private readonly ICacheService _cache;

    public EduMaterialService(IMapper mapper, IMinioRepository minioRepository,
        IEduMaterialRepository eduMaterialRepository, IUserGrpcClient userGrpcClient,
        IStudentHasEduMaterialRepository studentHasEduMaterialRepository, IElasticClient elasticClient, 
        ICacheService cache)
    {
        _mapper = mapper;
        _eduMaterialRepository = eduMaterialRepository;
        _userGrpcClient = userGrpcClient;
        _studentHasEduMaterialRepository = studentHasEduMaterialRepository;
        _minioRepository = minioRepository;
        _elasticClient = elasticClient;
        _cache = cache;
    }

    public async Task<int> UploadEduMaterialAsync(AddEduMaterialDTO dto, CancellationToken token)
    {
        var existed = await _eduMaterialRepository.GetAsync(material => material.Name == dto.Name);

        if (existed is not null)
        {
            throw new AlreadyExistsException();
        }
        
        var eduMaterial = _mapper.Map<EduMaterial>(dto);
        var added = await _eduMaterialRepository.AddAsync(eduMaterial);
        await _eduMaterialRepository.SaveAsync();

        var elkDto = _mapper.Map<EduMaterialDTO>(added);
        var resp = await _elasticClient.IndexAsync(elkDto, descriptor => descriptor.Id(elkDto.Id), token);

        if (!resp.IsValid)
        {
            throw new DocNotCreatedException();
        }
        
        await _cache.SetAsync(elkDto.Id.ToString(), elkDto, token);
        await _minioRepository.UploadReportAsync(dto.file.OpenReadStream(), eduMaterial.Name);

        return added.Id;    
    }

    public async Task<MemoryStream> DownloadEduMaterialAsync(int id, CancellationToken token)
    {
        var eduMaterialDTO = await _cache.GetAsync<EduMaterialDTO>(id.ToString(), token);
        string name;
        
        if (eduMaterialDTO is null)
        {
            var eduMaterial = await _eduMaterialRepository.GetAsync(em => em.Id == id);

            if (eduMaterial is null)
            {
                throw new EntityNotFoundException(paramname: nameof(id));
            }

            name = eduMaterial.Name;
        }
        else
        {
            name = eduMaterialDTO.Name;
        }

        var memoryStream = await _minioRepository.DownloadReportAsync(name, token);

        return memoryStream;
    }

    public async Task<List<EduMaterialDTO>> GetEduMaterialsByStudentAsync(int studentId, int pageNumber, int pageSize, CancellationToken token)
    {
        var cacheKey = JsonConvert.SerializeObject(new {student = studentId, num =  pageNumber, size = pageSize});
        var cacheResult = await _cache.GetAsync<List<EduMaterialDTO>>(cacheKey, token);

        if (cacheResult is not null)
        {
            return cacheResult;
        }
        
        var userReply = await _userGrpcClient.CheckUserAsync(studentId, token);

        if (!userReply.Exists)
        {
            throw new EntityNotFoundException(nameof(studentId));
        }

        if (userReply.Role != Roles.Student)
        {
            throw new WrongRoleForActionRequested(userReply.Role);
        }
        
        var eduMaterials = await _studentHasEduMaterialRepository
            .GetEduMaterialsByStudentAsync(studentId, pageNumber, pageSize);
        var eduMaterialsDTOs = _mapper.Map<List<EduMaterialDTO>>(eduMaterials);
        await _cache.SetAsync(cacheKey, eduMaterialsDTOs, token);
        
        return eduMaterialsDTOs; 
    }

    public async Task<List<EduMaterialDTO>> GetAllEduMaterialsAsync(int pageNumber, int pageSize, CancellationToken token)
    {
        var cacheKey = JsonConvert.SerializeObject(new {number = pageNumber, size = pageSize});
        var cacheResult = await _cache.GetAsync<List<EduMaterialDTO>>(cacheKey, token);

        if (cacheResult is not null)
        {
            return cacheResult;
        }
        
        var eduMaterials = await _eduMaterialRepository.GetAllAsync(pageNumber, pageSize);
        var dtos = _mapper.Map<List<EduMaterialDTO>>(eduMaterials);
        await _cache.SetAsync(cacheKey, dtos, token);
        
        return dtos;
    }

    public async Task AddEduMaterialToStudentAsync(AddEduMaterialToStudentDTO dto, CancellationToken token)
    {
        var userReply = await _userGrpcClient.CheckUserAsync(dto.StudentId, token);
        
        if (!userReply.Exists)
        {
            throw new EntityNotFoundException(nameof(dto.StudentId));
        }

        if (userReply.Role != Roles.Student)
        {
            throw new WrongRoleForActionRequested(userReply.Role);
        }

        var cacheResult = await _cache.GetAsync<EduMaterialDTO>(key: dto.Id.ToString(), cancellationToken: token);

        if (cacheResult is null)
        {
            var existingMaterial = await _eduMaterialRepository.GetByIdAsync(dto.Id);

            if (existingMaterial is null)
            {
                throw new EntityNotFoundException();
            }
        }
        
        var toAdd = _mapper.Map<StudentHasEduMaterial>(dto);
        await _studentHasEduMaterialRepository.AddAsync(toAdd);
        await _studentHasEduMaterialRepository.SaveAsync();
    }

    public async Task<IEnumerable<EduMaterialDTO>> SearchAsync(string text, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var cacheKey = JsonConvert.SerializeObject(new {text, pageNumber, pageSize});
        var cacheResponse = await _cache.GetAsync<IEnumerable<EduMaterialDTO>>(cacheKey, cancellationToken);

        if (cacheResponse is not null)
        {
            return cacheResponse;
        }
        
        var searchResponse = await _elasticClient.SearchAsync<EduMaterialDTO>(s => s
                .From((pageNumber - 1) * pageSize)
                .Size(pageSize)
                .Query(q => q
                    .MultiMatch(m => m
                        .Query(text)
                        .Fields(descriptor => descriptor
                            .Field(dto => dto.Name, boost: 15)
                            .Field(dto => dto.Theme))
                        .Type(TextQueryType.BestFields))
                ), cancellationToken
        );

        if (!searchResponse.IsValid)
        {
            throw searchResponse.OriginalException;
        }
        
        await _cache.SetAsync(cacheKey, searchResponse.Documents, cancellationToken);

        return searchResponse.Documents;
    }
}
