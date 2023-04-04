using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Infrastructure.Repositories
{
    /// <summary>
    /// Implements repositoy factory pattern for managing  creation and returning an instance of the specified repository type
    /// </summary>
    public class RepositoryFactory: IRepositoryFactory
    {
        private ConcurrentDictionary<Type, object>? _repositories;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialized a new instance of the <see cref="RepositoryFactory"/>
        /// </summary>
        /// <param name="serviceProvider">Service objects</param>
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            _repositories ??= new ConcurrentDictionary<Type, object>();

            var type = typeof(TRepository);

            return (TRepository)_repositories.GetOrAdd(type, _ => _serviceProvider.GetRequiredService<TRepository>());
        }
    }
}
