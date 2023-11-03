namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record OpeningDTO(int Id , int PsychologistId, DateTime Time , bool Active, DayOfWeek Day);