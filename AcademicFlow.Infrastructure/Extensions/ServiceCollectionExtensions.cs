using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicFlow.Infrastructure.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            return serviceCollection;
        }
    }
}
