using Mapster;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public class UpdateMeetupCommandHandler: IRequestHandler<UpdateMeetupCommand, int>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IScheduleCellRepository _scheduleCellRepository;
    
    public UpdateMeetupCommandHandler(IMeetupRepository meetupRepository,  IScheduleCellRepository scheduleCellRepository)
    {
        _meetupRepository = meetupRepository;
        _scheduleCellRepository = scheduleCellRepository;
    }

    public async Task<int> Handle(UpdateMeetupCommand request, CancellationToken cancellationToken)
    {
        var oldMeetup = await _meetupRepository.GetAsync(m => request.id == m.Id);

        if (oldMeetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.id));
        }

        if (oldMeetup.ScheduleCellId != request.MeetupDTO.ScheduleCellId)
        {
            var newScheduleCell = await _scheduleCellRepository.GetByIdAsync(request.MeetupDTO.ScheduleCellId);

            if (newScheduleCell is null)
            {
                throw new EntityNotFoundException(paramname: nameof(request.MeetupDTO.ScheduleCellId));
            }

            if (!HandlerHelper.IsScheduleCellAvailable(newScheduleCell))
            {
                throw new AlreadyExistsException();
            }
            
            if (request.MeetupDTO.Date.DayOfWeek != newScheduleCell.Day)
            {
                throw new WrongDateWeekdayException();
            }
        }
        
        var meetup = request.MeetupDTO.Adapt(oldMeetup);

        if (meetup is null)
        {
            throw new WrongRequestDataException();
        }

        await _meetupRepository.UpdateAsync(meetup);
        await _meetupRepository.SaveAsync();

        return meetup.Id;
    }
}
