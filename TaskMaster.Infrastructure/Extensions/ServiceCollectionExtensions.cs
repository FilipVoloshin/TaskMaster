﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Seeder.Abstractions;
using TaskMaster.Infrastructure.Seeder;
using Microsoft.Extensions.Logging.Abstractions;

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
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<ReadonlyTaskMasterDbContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
            services.AddScoped<ISeeder, DbSeeder>();

            return services;
        }

        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ReadonlyTaskMasterDbContext>();
            await new DbSeeder(dbContext, NullLogger<DbSeeder>.Instance).SeedAsync(cancellationToken);
        }

    }
}