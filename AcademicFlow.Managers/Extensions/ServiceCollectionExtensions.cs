using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Managers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicFlow.Managers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterManagers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserManager, UserManager>();
            serviceCollection.AddScoped<IUserCredentialsManager, UserCredentialsManager>();

            var mapper = GetMapperConfiguration().CreateMapper();
            serviceCollection.AddSingleton(mapper);

            return serviceCollection;
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            var types = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .Where(x => x.FullName!.StartsWith("AcademicFlow."))
                                .SelectMany(s => s.GetTypes())
                                .Where(p => typeof(Profile).IsAssignableFrom(p));

            return new MapperConfiguration(cfg =>
            {
                foreach (var type in types)
                    cfg.AddProfile(type);
            });
        }
    }
}
