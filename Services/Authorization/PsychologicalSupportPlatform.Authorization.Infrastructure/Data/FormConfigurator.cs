using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class FormConfigurator:IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.HasKey(u => new { u.Parallel, u.Letter });
        
        builder.HasMany(e => e.Students)
            .WithOne(e => e.Form)
            .HasForeignKey(e => new {e.Parallel, e.Letter})
            .IsRequired();
    }
}