using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class StudentConfigurator:IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
    }
}
