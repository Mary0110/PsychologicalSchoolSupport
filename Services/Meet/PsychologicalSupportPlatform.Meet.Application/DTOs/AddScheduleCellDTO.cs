namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddScheduleCellDTO(int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);
