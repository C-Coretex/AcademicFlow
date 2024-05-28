using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicFlow.Infrastructure.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<AcademicFlowDbContext>(options =>
                options.UseSqlServer(connectionString));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

            serviceCollection.AddScoped<ICourseProgramRepository, CourseProgramRepository>();
            serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
            serviceCollection.AddScoped<ICourseUserRoleRepository, CourseUserRoleRepository>();
            serviceCollection.AddScoped<IProgramRepository, ProgramRepository>();
            serviceCollection.AddScoped<IProgramUserRoleRepository, ProgramUserRoleRepository>();

            serviceCollection.AddScoped<IAssignmentTaskRepository, AssignmentTaskRepository>();
            serviceCollection.AddScoped<IAssignmentEntryRepository, AssignmentEntryRepository>();
            serviceCollection.AddScoped<IAssignmentGradeRepository, AssignmentGradeRepository>();

            return serviceCollection;
        }
    }
}
