using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByStudentIdQuery: IRequest<DataResponseInfo<List<MeetupDTO>>>
{
    public int StudentId { get; set; }
}
