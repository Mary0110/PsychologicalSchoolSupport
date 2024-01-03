using Microsoft.AspNetCore.Http;
using PsychologicalSupportPlatform.Edu.Application.DTOs;

namespace PsychologicalSupportPlatform.Edu.Application.Interfaces;

public interface IEduMaterialService
{
    Task<int> UploadEduMaterialAsync(AddEduMaterialDTO dto, CancellationToken token);
    
    Task<MemoryStream> DownloadEduMaterialAsync(int id, CancellationToken token);
    
    Task<List<EduMaterialDTO>> GetEduMaterialsByStudentAsync(GetEduMaterialByStudentDTO dto, CancellationToken token);

    Task<List<EduMaterialDTO>> GetAllEduMaterialsAsync(int pageNumber, int pageSize);

    Task AddEduMaterialToStudentAsync(AddEduMaterialToStudentDTO dto, CancellationToken token);
    
    Task<IEnumerable<EduMaterialDTO>> SearchAsync(SearchEduMaterialDTO dto, CancellationToken cancellationToken);
}
