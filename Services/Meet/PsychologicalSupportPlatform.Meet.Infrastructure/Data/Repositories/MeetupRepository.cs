using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Repositories;

public class MeetupRepository: SQLRepository<DataContext, Meetup>, IMeetupRepository
{
    public MeetupRepository(DataContext context) : base(context) { }
}
