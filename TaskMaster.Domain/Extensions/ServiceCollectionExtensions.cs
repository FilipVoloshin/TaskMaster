using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskMaster.Application.Abstractions;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Application.Services;

namespace TaskMaster.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services for the TaskMaster API.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>Contract for a collection of service descriptors</returns>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IUserContext, UserContext>();
            services.AddValidatorsFromAssembly(executingAssembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(executingAssembly));
            services.AddAutoMapper(executingAssembly);

            return services;
        }
    }
}
