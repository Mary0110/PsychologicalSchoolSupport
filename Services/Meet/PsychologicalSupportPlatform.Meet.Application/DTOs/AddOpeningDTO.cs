namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record AddOpeningDTO(int PsychologistId, DateTime Time , bool Active, DayOfWeek Day);