namespace PsychologicalSupportPlatform.Common;

public record ReportMeetupDTO(
    string CreatorName,
    string CreatorSurname,
    string? CreatorPatronymic,
    DateOnly Date,
    string StudentName,
    string StudentSurname,
    string? StudentPatronymic,
    string Comments,
    string Conclusion
    );
