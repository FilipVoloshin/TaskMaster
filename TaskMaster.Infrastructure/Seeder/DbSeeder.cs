using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskMaster.Infrastructure.Contexts;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Seeder.Abstractions;

namespace TaskMaster.Infrastructure.Seeder
{
    /// <summary>
    /// A class that seeds data into the TaskMaster database.
    /// </summary>
    internal class DbSeeder : ISeeder
    {
        private readonly TaskMasterDbContext _dbContext;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(TaskMasterDbContext dbContext, ILogger<DbSeeder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_dbContext.Database.IsRelational())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                    await SeedDatabaseDataAsync(cancellationToken);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while seeding the database for {nameof(TaskMasterDbContext)}");
            }
        }

        #region Private methods

        private async Task SeedDatabaseDataAsync(CancellationToken cancellationToken)
        {
            var (users, taskLists) = GetUsersAndTaskLists();
            var assignedTaskLists = GetAssignedTaskLists(users, taskLists);

            await AddDataIfTableIsEmptyAsync(_dbContext.Users, users, cancellationToken);
            await AddDataIfTableIsEmptyAsync(_dbContext.TaskLists, taskLists, cancellationToken);
            await AddDataIfTableIsEmptyAsync(_dbContext.AssignedTaskLists, assignedTaskLists, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task AddDataIfTableIsEmptyAsync<T>(DbSet<T> dbSet, List<T> data, CancellationToken cancellationToken) where T : class
        {
            if (!dbSet.Any())
            {
                await dbSet.AddRangeAsync(data, cancellationToken);
            }
        }

        private (List<User>, List<TaskList>) GetUsersAndTaskLists()
        {
            var users = new List<User>
            {
                new() { Id = Guid.NewGuid(), Name = "Main Admin User" },
                new() { Id = Guid.NewGuid(), Name = "Philip Voloshyn" }
            };

            var taskLists = new List<TaskList>
            {
                new() { Id = Guid.NewGuid(), Name = "Urgent Tasks", AuthorId = users[0].Id },
                new() { Id = Guid.NewGuid(), Name = "Important Tasks", AuthorId = users[0].Id },
                new() { Id = Guid.NewGuid(), Name = "Secondary Tasks", AuthorId = users[0].Id },
                new() { Id = Guid.NewGuid(), Name = "Random Tasks", AuthorId = users[1].Id }
            };

            return (users, taskLists);
        }

        private List<AssignedTaskList> GetAssignedTaskLists(List<User> users, List<TaskList> taskLists)
        {
            return new List<AssignedTaskList>
            {
                new() { UserId = users[0].Id, TaskListId = taskLists[0].Id },
                new() { UserId = users[0].Id, TaskListId = taskLists[1].Id },
                new() { UserId = users[0].Id, TaskListId = taskLists[2].Id },
                new() { UserId = users[1].Id, TaskListId = taskLists[3].Id }
            };
        }

        #endregion
    }
}
