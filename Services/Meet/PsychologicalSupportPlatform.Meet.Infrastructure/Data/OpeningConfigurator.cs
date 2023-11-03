using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;

public class OpeningConfigurator: IEntityTypeConfiguration<Opening>
{
    public void Configure(EntityTypeBuilder<Opening> builder)
    {
        builder.HasMany(e => e.Meetups)
            .WithOne(e => e.Opening)
            .HasForeignKey(e => e.OpeningId);
    }
}
