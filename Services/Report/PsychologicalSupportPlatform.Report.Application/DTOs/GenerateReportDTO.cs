namespace PsychologicalSupportPlatform.Report.Application.DTOs;

public record GenerateReportDTO(int meetId, string comment, string conclusion, int creatorId);
