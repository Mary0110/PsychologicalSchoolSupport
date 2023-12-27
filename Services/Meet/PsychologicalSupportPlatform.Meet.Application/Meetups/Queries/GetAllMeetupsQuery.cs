using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public record GetAllMeetupsQuery(int PageNumber, int PageSize): IRequest<List<MeetupDTO>>;
