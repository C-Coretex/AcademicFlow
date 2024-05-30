using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class AssignmentEntryConfiguration : IEntityTypeConfiguration<AssignmentEntry>
    {
        public void Configure(EntityTypeBuilder<AssignmentEntry> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.AssignmentTask).WithMany(x => x.AssignmentEntries)
                   .HasForeignKey(x => x.AssignmentTaskId)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.User).WithMany(x => x.AssignmentEntries)
                   .HasForeignKey(x => x.CreatedById)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasIndex(x => x.CreatedById);
        }
    }
}
