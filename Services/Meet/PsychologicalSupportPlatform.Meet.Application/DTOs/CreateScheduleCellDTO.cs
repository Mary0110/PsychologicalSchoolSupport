namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record CreateScheduleCellDTO(int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);
