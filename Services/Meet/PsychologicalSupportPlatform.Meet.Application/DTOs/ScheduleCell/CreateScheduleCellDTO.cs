namespace PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

public record CreateScheduleCellDTO(int PsychologistId, int Hours, int Minutes, bool Active, DayOfWeek Day);
