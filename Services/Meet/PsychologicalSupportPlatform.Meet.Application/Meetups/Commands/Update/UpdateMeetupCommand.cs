using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public record UpdateMeetupCommand(MeetupDTO MeetupDTO): IRequest;
