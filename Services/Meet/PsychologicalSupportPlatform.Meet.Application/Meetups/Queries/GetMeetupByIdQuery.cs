using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupByIdQuery: IRequest<MeetupDTO>
{
    public int Id { get; set; }
}
