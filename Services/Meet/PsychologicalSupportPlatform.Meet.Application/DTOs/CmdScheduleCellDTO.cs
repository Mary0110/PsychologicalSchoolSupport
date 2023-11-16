namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record CmdScheduleCellDTO(int Id, int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);