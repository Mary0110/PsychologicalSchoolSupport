using MapsterMapper;
using Microsoft.AspNetCore.Http;
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

    public async Task<int> UploadEduMaterialAsync(AddEduMaterialDTO dto)
    {
        var eduMaterial = _mapper.Map<EduMaterial>(dto);

        var added = await _eduMaterialRepository.AddAsync(eduMaterial);
        await _eduMaterialRepository.SaveAsync();

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

    public async Task<List<EduMaterialDTO>> GetEduMaterialsByStudentAsync(int studentId, int pageNumber, int pageSize, CancellationToken token)
    {
        var userReply = await _userGrpcClient.CheckUserAsync(studentId, token);

        if (!userReply.Exists)
        {
            throw new EntityNotFoundException(nameof(studentId));
        }

        if (userReply.Role != Roles.Student)
        {
            throw new WrongRoleForActionRequested(userReply.Role);
        }
        
        var eduMaterials = await _studentHasEduMaterialRepository.GetEduMaterialsByStudentAsync(studentId, pageNumber, pageSize);
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

    public async Task<IEnumerable<EduMaterialDTO>> SearchAsync(string text, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<EduMaterialDTO>(s => s
            .Query(q => q
                .QueryString(qs => qs
                    .Fields(f => f
                        .Field(ff => ff.Name)
                        .Field(ff => ff.Theme)
                    )
                    .Query($"*{text}*")
                )
            )
        );
        if (searchResponse.IsValid)
        {
            return searchResponse.Documents;
        }
        else
        {
            throw new Exception($"Failed to perform search: {searchResponse.DebugInformation}");
        }    
    }
}
