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

            return serviceCollection;
        }
    }
}
