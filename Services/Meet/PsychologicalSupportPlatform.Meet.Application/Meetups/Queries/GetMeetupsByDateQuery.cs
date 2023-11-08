using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByDateQuery: IRequest<DataResponseInfo<List<MeetupDTO>>>
{
    public DateOnly Date{ get; set; }
}
