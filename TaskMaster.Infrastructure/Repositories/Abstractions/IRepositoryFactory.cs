namespace TaskMaster.Infrastructure.Repositories.Abstractions
{
    /// <summary>
    /// Factory interface to create repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets an instance of the repository of the specified type.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository to get.</typeparam>
        /// <returns>An instance of the repository of the specified type.</returns>
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
