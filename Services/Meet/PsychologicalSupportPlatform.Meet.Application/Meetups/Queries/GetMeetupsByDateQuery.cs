using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupsByDateQuery: IRequest<List<MeetupDTO>>
{
    public DateTime Date{ get; set; }
}
