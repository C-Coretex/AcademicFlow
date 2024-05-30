using AcademicFlow.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicFlow.Infrastructure.Configuration
{
    public class AssignmentGradeConfiguration : IEntityTypeConfiguration<AssignmentGrade>
    {
        public void Configure(EntityTypeBuilder<AssignmentGrade> builder)
        {
            builder.HasKey(u => u.Id); 
            builder.HasOne(x => x.AssignmentEntry).WithOne(x => x.AssignmentGrade)
                   .HasForeignKey<AssignmentGrade>(x => x.AssignmentEntryId)
                   .HasPrincipalKey<AssignmentEntry>(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.User).WithMany(x => x.AssignmentGrades)
                   .HasForeignKey(x => x.GradedById)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasIndex(x => x.GradedById);
        }
    }
}
