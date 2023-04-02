using TaskMaster.Application.MediatR.Base;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Infrastructure.Entities;
using TaskMaster.Infrastructure.Repositories.Abstractions;

namespace TaskMaster.Application.MediatR.TaskLists.Handlers
{
    public class CreateTaskListHandler : BaseRequestHandler<CreateTaskListCommand, Guid>
    {
        public CreateTaskListHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task<Guid> HandleAsync(CreateTaskListCommand request, CancellationToken cancellationToken = default)
        {
            var taskList = Mapper.Map<TaskList>(request);

            taskList.AuthorId = CurrentUserId;

            var id =  await UnitOfWork.GetRepository<ICommandRepository<TaskList>>()
                .AddAsync(taskList, cancellationToken);

            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return id;

        }
    }
}
