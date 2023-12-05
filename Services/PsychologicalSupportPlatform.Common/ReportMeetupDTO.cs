namespace PsychologicalSupportPlatform.Common;

public record ReportMeetupDTO(
    string creatorName,
    string creatorSurname,
    string? creatorPatronymic,
    DateOnly Date,
    string StudentName,
    string StudentSurname,
    string? StudentPatronymic,
    string Comments,
    string Conclusion
    );
