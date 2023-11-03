namespace PsychologicalSupportPlatform.Meet.Domain.Entities;

public class Meetup
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public int OpeningId { get; set; }

    public Opening Opening { get; set; }

    public int StudentId { get; set; }
}
