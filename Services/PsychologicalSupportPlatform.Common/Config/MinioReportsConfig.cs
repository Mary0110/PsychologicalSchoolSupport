namespace PsychologicalSupportPlatform.Report.Infrastructure.Config;

public class MinioReportsConfig
{
    public string Endpoint { get; set; } = string.Empty;
    
    public string RootPass { get; set; } = string.Empty;
    
    public string RootUser { get; set; } = string.Empty;

    public string BucketName { get; set; } = string.Empty;
}
