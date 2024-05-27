using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Base;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Infrastructure
{
    public class AcademicFlowDbContext(DbContextOptions<AcademicFlowDbContext> options) : DbContextBase<AcademicFlowDbContext>(options)
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<CourseProgram> CourseProgram { get; set; }
        public DbSet<ProgramUserRole> ProgramUserRole { get; set; }
        public DbSet<Program> Program { get; set; }
        public DbSet<CourseUserRole> CourseUserRole { get; set; }
        public DbSet<Course> Course { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Applies all configurations defined in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
