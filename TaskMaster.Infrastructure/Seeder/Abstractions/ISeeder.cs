namespace TaskMaster.Infrastructure.Seeder.Abstractions
{
    /// <summary>
    /// Provides a way to seed initial data into a database context.
    /// </summary>
    internal interface ISeeder
    {
        /// <summary>
        /// Seeds initial data into the specified database context.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
