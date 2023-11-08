namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record OpeningDTO(int Id , int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);