using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupByIdQuery: IRequest<MeetupDTO>
{
    public int Id { get; set; }
}
