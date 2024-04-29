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

            return serviceCollection;
        }
    }
}
