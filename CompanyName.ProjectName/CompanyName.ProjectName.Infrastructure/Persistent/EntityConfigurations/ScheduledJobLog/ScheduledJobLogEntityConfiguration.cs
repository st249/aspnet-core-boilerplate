using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CompanyName.ProjectName.Infrastructure.Persistent.EntityConfigurations.ScheduledJobLog;

public class ScheduledJobLogEntityConfiguration : IEntityTypeConfiguration<Domain.Aggregates.ScheduledJobLogAggregate.ScheduledJobLog>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Aggregates.ScheduledJobLogAggregate.ScheduledJobLog> builder)
    {
        builder.ToTable("ScheduledJobLogs", ProjectNameContext.DEFAULT_SCHEMA);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.JobName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Status)
            .HasDefaultValue(ScheduledJobLogStatus.InProgress);

        builder.Property(e => e.Status)
            .HasConversion(e => e.Id, value => ScheduledJobLogStatus.From(value));
    }
}


