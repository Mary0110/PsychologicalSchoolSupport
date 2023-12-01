namespace PsychologicalSupportPlatform.Common;

public record ReportMeetupDTO(
    DateOnly Date,
    string StudentName,
    string StudentSurname,
    string Comments,
    string Conclusion);
