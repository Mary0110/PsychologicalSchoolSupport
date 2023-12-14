namespace PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

public record ScheduleCellDTO(int Id, int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);
