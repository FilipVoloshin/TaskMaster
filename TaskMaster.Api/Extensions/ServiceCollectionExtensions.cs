using TaskMaster.Domain.Abstractions;
using TaskMaster.Domain.Services;

namespace TaskMaster.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services for the TaskMaster API.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>Contract for a collection of service descriptors</returns>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            return services;
        }
    }
}
