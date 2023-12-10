using MediatR;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Approve;

public class ApproveMeetupByStudentCommandHandler: IRequestHandler<ApproveMeetupByStudentCommand, int>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IRabbitMQMessagingService _service;
    
    public ApproveMeetupByStudentCommandHandler(IMeetupRepository meetupRepository, IRabbitMQMessagingService service)
    {
        _meetupRepository = meetupRepository;
        _service = service;
    }

    public async Task<int> Handle(ApproveMeetupByStudentCommand request, CancellationToken cancellationToken)
    {
        var oldMeetup = await _meetupRepository.GetAsync(m => request.MeetupDTO.meetupId == m.Id);

        if (oldMeetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(oldMeetup.Id));
        }

        if (oldMeetup.StudentId != request.MeetupDTO.studentId)
        {
            throw new WrongMeetupForTheStudent();
        }

        if (!HasStarted(oldMeetup))
        {
            throw new MeetupHasntStartedException();
        }

        if (oldMeetup.IsApprovedByStudent)
        {
            throw new IsAlreadyApprovedException();
        }

        oldMeetup.IsApprovedByStudent = true;
        await _meetupRepository.UpdateAsync(oldMeetup);
        await _meetupRepository.SaveAsync();
        
        var messageObject = new MeetupMessageObject
        {
            StudentId = oldMeetup.StudentId,
            MeetupId = oldMeetup.Id,
            Date = oldMeetup.Date.ToDateTime(oldMeetup.ScheduleCell.Time)
        };
        
        var messageJson = JsonConvert.SerializeObject(messageObject);
        _service.PublishMessage("meetup-info", messageJson);

        return oldMeetup.Id;
    }

    private static bool HasStarted(Meetup meetup)
    {
        if (meetup.ScheduleCell.Time <= TimeOnly.FromDateTime(DateTime.Now) &&
            meetup.Date == DateOnly.FromDateTime(DateTime.Today))
        {
            return true;
        }

        return false;
    }
}