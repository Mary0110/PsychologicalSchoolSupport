namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IMinioRepository
{
    Task UploadReportAsync(Stream data, string objectName);
    
    Task<MemoryStream> DownloadReportAsync(string objectName, CancellationToken token);
}
