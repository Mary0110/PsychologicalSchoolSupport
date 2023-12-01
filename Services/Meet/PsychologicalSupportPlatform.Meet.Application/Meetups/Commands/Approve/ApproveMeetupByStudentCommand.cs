using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Approve;

public record ApproveMeetupByStudentCommand(ApproveMeetupDTO MeetupDTO): IRequest<int>;
