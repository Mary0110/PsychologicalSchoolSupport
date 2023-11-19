namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record UpdateScheduleCellDTO(int Id, int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);