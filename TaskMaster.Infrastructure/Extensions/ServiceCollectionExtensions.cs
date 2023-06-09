﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Seeder.Abstractions;
using TaskMaster.Infrastructure.Seeder;
using Microsoft.Extensions.Logging.Abstractions;
using TaskMaster.Infrastructure.Repositories.Abstractions;
using TaskMaster.Infrastructure.Repositories;
using TaskMaster.Infrastructure.UnitsOfWork;
using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;

namespace TaskMaster.Infrastructure.Extensions
{
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
            var commandConnectionString = configuration.GetConnectionString("CommandPostgreSQLConnection");
            var queryConnectionString = configuration.GetConnectionString("QueryPostgreSQLConnection");

            services.AddDbContextPool<CommandTaskMasterDbContext>(options => options.UseNpgsql(commandConnectionString).UseSnakeCaseNamingConvention());
            services.AddDbContextPool<QueryTaskMasterDbContext>(options => options.UseNpgsql(queryConnectionString).UseSnakeCaseNamingConvention());

            services.AddAutoMapper(typeof(BaseRepository<>).Assembly);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(IProjectionQueryRepository<>), typeof(ProjectionQueryRepository<>));
            services.AddScoped<ISpecificationEvaluator>(sp => new SpecificationEvaluator(true));

            services.AddScoped<ISeeder, DbSeeder>();

            return services;
        }

        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommandTaskMasterDbContext>();
            await new DbSeeder(dbContext, NullLogger<DbSeeder>.Instance).SeedAsync(cancellationToken);
        }

    }
}