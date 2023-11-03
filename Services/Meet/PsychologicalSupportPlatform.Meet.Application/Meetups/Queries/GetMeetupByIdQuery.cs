using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupByIdQuery: IRequest<MeetupDTO>
{
    public int Id { get; set; }
}