using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public record GetMeetupsByStudentIdQuery(int StudentId, int pageNumber, int pageSize): IRequest<List<MeetupDTO>> { }
