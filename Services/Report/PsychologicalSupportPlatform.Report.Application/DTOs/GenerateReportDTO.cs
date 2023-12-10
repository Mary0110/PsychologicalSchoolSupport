namespace PsychologicalSupportPlatform.Report.Application.DTOs;

public record GenerateReportDTO(int MeetId, string Comment, string Conclusion, int CreatorId);
