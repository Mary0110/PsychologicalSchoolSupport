using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public record GetAllMeetupsQuery(int pageNumber, int pageSize): IRequest<List<MeetupDTO>> { }