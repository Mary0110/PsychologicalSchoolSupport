using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public record UpdateMeetupCommand(int Id, AddMeetupDTO MeetupDTO): IRequest<int>;
