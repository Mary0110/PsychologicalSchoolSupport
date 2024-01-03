using MediatR;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public record DeleteMeetupCommand(int Id): IRequest<int>;
