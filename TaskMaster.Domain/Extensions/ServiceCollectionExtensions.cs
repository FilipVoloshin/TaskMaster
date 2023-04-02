using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Application.MediatR;

namespace TaskMaster.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services for the MediatR services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>Contract for a collection of service descriptors</returns>
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            var currentAssembly = typeof(BaseRequestHandler<,>).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));
            services.AddAutoMapper(currentAssembly);


            return services;
        }
    }
}
