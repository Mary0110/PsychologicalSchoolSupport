using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data.Configurators;

public class MeetupReportConfigurator: IEntityTypeConfiguration<MeetupReport>
{
    public void Configure(EntityTypeBuilder<MeetupReport> builder)
    {
        //TODO:delete or finish
    }
}
