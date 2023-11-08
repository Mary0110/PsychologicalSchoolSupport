using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;

public class CreateMeetupCommand : IRequest<ResponseInfo>
{
    public AddMeetupDTO meetupDto { get; set; }
}
