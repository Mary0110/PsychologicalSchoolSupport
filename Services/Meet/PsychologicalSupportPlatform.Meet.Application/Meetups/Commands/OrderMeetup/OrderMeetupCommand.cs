using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;

public record OrderMeetupCommand(AddMeetupDTO MeetupDto) : IRequest<int>;
