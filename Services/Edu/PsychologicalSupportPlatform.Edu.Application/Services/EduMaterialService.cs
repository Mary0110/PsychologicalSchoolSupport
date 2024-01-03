using MapsterMapper;
using Nest;
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


    public EduMaterialService(IMapper mapper, IMinioRepository minioRepository,
        IEduMaterialRepository eduMaterialRepository, IUserGrpcClient userGrpcClient,
        IStudentHasEduMaterialRepository studentHasEduMaterialRepository, IElasticClient elasticClient)
    {
        _mapper = mapper;
        _eduMaterialRepository = eduMaterialRepository;
        _userGrpcClient = userGrpcClient;
        _studentHasEduMaterialRepository = studentHasEduMaterialRepository;
        _minioRepository = minioRepository;
        _elasticClient = elasticClient;
    }

    public async Task<int> UploadEduMaterialAsync(AddEduMaterialDTO dto, CancellationToken token)
    {
        var eduMaterial = _mapper.Map<EduMaterial>(dto);

        var added = await _eduMaterialRepository.AddAsync(eduMaterial);
        await _eduMaterialRepository.SaveAsync();

        var elkDto = _mapper.Map<EduMaterialDTO>(added);
        var resp = await _elasticClient.IndexAsync(elkDto, descriptor => 
            descriptor.Id(elkDto.Id), token);
       
        if (!resp.IsValid)
        {
            throw new DocNotCreatedException();
        }
        
        await _minioRepository.UploadReportAsync(dto.file.OpenReadStream(), eduMaterial.Name);

        return added.Id;    
    }

    public async Task<MemoryStream> DownloadEduMaterialAsync(int id, CancellationToken token)
    {
        var eduMaterial = await _eduMaterialRepository.GetAsync(em => em.Id == id);
        
        if (eduMaterial is null)
        {
            throw new EntityNotFoundException(paramname: nameof(id));
        }

        var memoryStream = await _minioRepository.DownloadReportAsync(eduMaterial.Name, token);

        return memoryStream;
    }

    public async Task<List<EduMaterialDTO>> GetEduMaterialsByStudentAsync(GetEduMaterialByStudentDTO dto, 
        CancellationToken token)
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
        
        var eduMaterials = await _studentHasEduMaterialRepository.GetEduMaterialsByStudentAsync(
            dto);
        var eduMaterialsDTOs = _mapper.Map<List<EduMaterialDTO>>(eduMaterials);
        
        return eduMaterialsDTOs; 
    }

    public async Task<List<EduMaterialDTO>> GetAllEduMaterialsAsync(int pageNumber, int pageSize)
    {
        var eduMaterials = await _eduMaterialRepository.GetAllAsync(pageNumber, pageSize);
        var dtos = _mapper.Map<List<EduMaterialDTO>>(eduMaterials);

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

        var shm = _mapper.Map<StudentHasEduMaterial>(dto);
        await _studentHasEduMaterialRepository.AddAsync(shm);
        await _studentHasEduMaterialRepository.SaveAsync();
    }

    public async Task<IEnumerable<EduMaterialDTO>> SearchAsync(SearchEduMaterialDTO dto, CancellationToken cancellationToken)
    {
        const int boost = 15;
        
        var searchResponse = await _elasticClient.SearchAsync<EduMaterialDTO>(s => s
                .From((dto.PageNumber - 1) * dto.PageSize)
                .Size(dto.PageSize)
                .Query(q => q
                    .MultiMatch(m=>m
                        .Query(dto.Text)
                        .Fields(descriptor => descriptor
                            .Field(material => material.Name, boost: boost)
                            .Field(material => material.Theme))
                        .Type(TextQueryType.BestFields))
                ), cancellationToken
        );
        
        if (searchResponse.IsValid)
        {
            return searchResponse.Documents;
        }
        else
        {
            var exc = searchResponse.OriginalException;
            throw exc;
        }    
    }
}
