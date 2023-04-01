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
        private readonly ReadonlyTaskMasterDbContext _dbContext;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(ReadonlyTaskMasterDbContext dbContext, ILogger<DbSeeder> logger)
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
                _logger.LogError(ex, $"An error occurred while seeding the database for {nameof(ReadonlyTaskMasterDbContext)}");
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

        private static async Task AddDataIfTableIsEmptyAsync<T>(DbSet<T> dbSet, List<T> data, CancellationToken cancellationToken) where T : class
        {
            if (!dbSet.Any())
            {
                await dbSet.AddRangeAsync(data, cancellationToken);
            }
        }

        private static  (List<User>, List<TaskList>) GetUsersAndTaskLists()
        {
            var users = new List<User>
            {
                new() { Id = Guid.NewGuid(), Name = "Main Admin User" },
                new() { Id = Guid.NewGuid(), Name = "Philip Voloshyn" }
            };

            var startDate = DateTime.UtcNow.AddMonths(-1);
            var endDate = DateTime.UtcNow;

            var taskLists = new List<TaskList>
            {
                new() { Id = Guid.NewGuid(), Name = "Urgent Tasks", AuthorId = users[0].Id, CreatedAtUtc = RandomDate(startDate, endDate) },
                new() { Id = Guid.NewGuid(), Name = "Important Tasks", AuthorId = users[0].Id, CreatedAtUtc = RandomDate(startDate, endDate) },
                new() { Id = Guid.NewGuid(), Name = "Secondary Tasks", AuthorId = users[0].Id, CreatedAtUtc = RandomDate(startDate, endDate) },
                new() { Id = Guid.NewGuid(), Name = "Random Tasks", AuthorId = users[1].Id, CreatedAtUtc = RandomDate(startDate, endDate) }
            };

            return (users, taskLists);
        }

        private static List<AssignedTaskList> GetAssignedTaskLists(List<User> users, List<TaskList> taskLists)
        {
            return new List<AssignedTaskList>
            {
                new() { AssigneeId = users[1].Id, TaskListId = taskLists[0].Id, AuthorId = users[0].Id  },
                new() { AssigneeId = users[0].Id, TaskListId = taskLists[1].Id, AuthorId = users[0].Id  },
                new() { AssigneeId = users[1].Id, TaskListId = taskLists[2].Id, AuthorId = users[0].Id  },
                new() { AssigneeId = users[1].Id, TaskListId = taskLists[3].Id, AuthorId = users[1].Id }
            };
        }

        private static DateTime RandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            int randomDay = new Random().Next(range);

            return start.AddDays(randomDay);
        }

        #endregion
    }
}
