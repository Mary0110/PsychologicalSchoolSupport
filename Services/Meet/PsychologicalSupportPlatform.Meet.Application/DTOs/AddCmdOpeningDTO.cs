namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddCmdOpeningDTO(int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);