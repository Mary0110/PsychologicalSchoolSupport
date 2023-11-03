using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;

public class CreateMeetupCommand : IRequest<int>
{
    public AddMeetupDTO meetupDto { get; set; }
}
