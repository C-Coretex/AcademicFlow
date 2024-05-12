using AcademicFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;

namespace AcademicFlow.Infrastructure.Configuration
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.User)
                  .WithMany(u => u.UserRoles)
                  .HasForeignKey(u => u.UserId)
                  .HasPrincipalKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.HasIndex(u => u.UserId).IsUnique();
            builder.Property(u => u.Active).IsRequired();
            builder.Property(u => u.Role).IsRequired();


        }
    }
}
