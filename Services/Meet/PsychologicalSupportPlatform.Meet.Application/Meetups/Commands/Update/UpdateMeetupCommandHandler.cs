using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public class UpdateMeetupCommandHandler: IRequestHandler<UpdateMeetupCommand, int>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public UpdateMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper,  IScheduleCellRepository scheduleCellRepository)
    {
        this.meetupRepository = meetupRepository;
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(UpdateMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = mapper.Map<Meetup>(request.MeetupDTO);

        if (meetup is null)
        {
            throw new WrongRequestDataException();
        }

        var oldMeetup = meetupRepository.GetByIdAsync(meetup.Id);

        if (oldMeetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(meetup.Id));
        }

        var newScheduleCell = await scheduleCellRepository.GetByIdAsync(meetup.ScheduleCellId);

        if (newScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(meetup.ScheduleCellId));
        }
        
        meetupRepository.Update(meetup);
        await meetupRepository.SaveAsync();

        return meetup.Id;
    }
}
