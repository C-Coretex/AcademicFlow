using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicFlow.Domain.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserCredentialsService, UserCredentialsService>();
            serviceCollection.AddScoped<ICourseProgramService, CourseProgramService>();
            serviceCollection.AddScoped<ICourseService, CourseService>();
            serviceCollection.AddScoped<IProgramService, ProgramService>();

            return serviceCollection;
        }
    }
}
