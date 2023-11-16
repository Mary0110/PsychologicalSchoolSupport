namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddCmdScheduleCellDTO(int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);