namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddOpeningDTO(int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);
