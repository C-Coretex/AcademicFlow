using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicFlow.Managers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterManagers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserManager, UserManager>();

            return serviceCollection;
        }
    }
}
