using AcademicFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Migrations.Factory
{
    public class MigrationsDbContext : DbContext
    {
        public MigrationsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademicFlowDbContext).Assembly);
        }
    }
}
