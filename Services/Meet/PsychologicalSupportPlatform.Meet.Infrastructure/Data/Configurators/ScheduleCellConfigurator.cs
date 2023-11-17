using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Configurators;

public class ScheduleCellConfigurator: IEntityTypeConfiguration<ScheduleCell>
{
    public void Configure(EntityTypeBuilder<ScheduleCell> builder)
    {
        builder.HasMany(e => e.Meetups)
            .WithOne(e => e.ScheduleCell)
            .HasForeignKey(e => e.ScheduleCellId);
        builder.Navigation(e => e.Meetups).AutoInclude();
    }
}
