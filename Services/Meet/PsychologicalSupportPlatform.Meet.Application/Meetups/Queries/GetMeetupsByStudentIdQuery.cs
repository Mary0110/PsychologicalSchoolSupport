using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public record GetMeetupsByStudentIdQuery(int StudentId, int pageNumber, int pageSize): IRequest<List<MeetupDTO>> { }
