using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetupByPsychologist;

public record OrderMeetupByPsychologistCommand(AddMeetupDTO MeetupDto, string PsyId) : IRequest<int>;
