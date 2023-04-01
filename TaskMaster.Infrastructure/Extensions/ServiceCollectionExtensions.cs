using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Infrastructure.Contexts;

namespace TaskMaster.Infrastructure.Extensions
{
    /// <summary>
    /// Extension method to register infrastructure services in the DI container.
    /// </summary>
    /// <param name="services">The collection of service descriptors.</param>
    /// <param name="configuration">The configuration root object.</param>
    /// <returns>The modified collection of service descriptors.</returns>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the infrastructure services for the application.
        /// </summary>
        /// <param name="services">The service collection to register the services to.</param>
        /// <param name="configuration">The configuration instance.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskMasterDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"))
                .UseSnakeCaseNamingConvention());

            return services;
        }
    }
}