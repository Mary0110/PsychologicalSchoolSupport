namespace PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

public record AddScheduleCellDTO(int PsychologistId, TimeOnly Time, bool Active, DayOfWeek Day);
