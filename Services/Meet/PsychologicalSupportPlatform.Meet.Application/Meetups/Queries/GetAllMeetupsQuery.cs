using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetAllMeetupsQuery: IRequest<List<MeetupDTO>>
{
}