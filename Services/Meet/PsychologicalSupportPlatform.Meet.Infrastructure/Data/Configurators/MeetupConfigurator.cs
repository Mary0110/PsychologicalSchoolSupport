using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Configurators;

public class MeetupConfigurator: IEntityTypeConfiguration<Meetup>
{
    public void Configure(EntityTypeBuilder<Meetup> builder)
    {
        builder.Navigation(m => m.ScheduleCell).AutoInclude();
    }
}
