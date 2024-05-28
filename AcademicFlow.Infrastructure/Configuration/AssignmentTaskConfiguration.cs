using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class AssignmentTaskConfiguration : IEntityTypeConfiguration<AssignmentTask>
    {
        public void Configure(EntityTypeBuilder<AssignmentTask> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(x => x.User).WithMany(x => x.AssignmentTasks)
                   .HasForeignKey(x => x.CreatedById)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasIndex(x => x.CreatedById);
            builder.HasIndex(x => x.CourseId);
        }
    }
}
