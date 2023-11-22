using MediatR;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public record DeleteMeetupCommand(int Id): IRequest<int>;