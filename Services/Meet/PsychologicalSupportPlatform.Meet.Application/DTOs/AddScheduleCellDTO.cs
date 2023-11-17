namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddScheduleCellDTO(int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);