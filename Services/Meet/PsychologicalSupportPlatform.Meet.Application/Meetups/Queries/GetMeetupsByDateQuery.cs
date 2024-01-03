using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public record GetMeetupsByDateQuery(DateOnly Date, int PageNumber, int PageSize) : IRequest<List<MeetupDTO>>;
