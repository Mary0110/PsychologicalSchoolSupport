using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupByStudentIdQuery: IRequest<List<MeetupDTO>>
{
    public int StudentId { get; set; }
}