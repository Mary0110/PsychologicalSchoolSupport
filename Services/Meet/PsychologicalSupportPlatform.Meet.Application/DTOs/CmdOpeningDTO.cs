namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record CmdOpeningDTO(int Id, int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);