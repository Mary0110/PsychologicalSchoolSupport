namespace PsychologicalSupportPlatform.Meet.Application.DTOs;

public record ScheduleCellDTO(int Id , int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);