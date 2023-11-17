using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;

public class OrderMeetupCommand: IRequest<int>
{ 
    public AddMeetupDTO MeetupDto { get; set; }
}
