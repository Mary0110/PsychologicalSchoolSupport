namespace PsychologicalSupportPlatform.Meet.Domain.Entities;

public class Opening
{
    public int Id { get; set; }
    
    public int PsychologistId { get; set; }
    
    public DateTime Time { get; set; }
    
    public bool Active { get; set; }
    
    public DayOfWeek Day { get; set; }
    
    public List<Meetup> Meetups { get; set; }
}
