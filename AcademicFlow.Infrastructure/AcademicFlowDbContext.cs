using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Base;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Infrastructure
{
    public class AcademicFlowDbContext: DbContextBase<AcademicFlowDbContext>
    {
        public AcademicFlowDbContext(DbContextOptions<AcademicFlowDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Applies all configurations defined in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
