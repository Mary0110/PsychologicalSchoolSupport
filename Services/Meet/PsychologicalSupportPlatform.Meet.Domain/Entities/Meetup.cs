namespace PsychologicalSupportPlatform.Meet.Domain.Entities;

public class Meetup
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public int ScheduleCellId { get; set; }

    public ScheduleCell ScheduleCell { get; set; }

    public int StudentId { get; set; }

    public bool IsApprovedByStudent { get; set; }
}
