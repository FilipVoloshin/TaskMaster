using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Extensions;
using TaskMaster.Api.Models;
using TaskMaster.Application.Extensions;
using TaskMaster.Application.MediatR.AssignedTaskLists.Command;
using TaskMaster.Application.MediatR.AssignedTaskLists.Queries;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Application.MediatR.TaskLists.Queries;
using TaskMaster.Infrastructure.Extensions;
using TaskMaster.Swagger.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
 .AddSwaggerModule()
 .RegisterInfrastructure(builder.Configuration)
 .RegisterApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    if (args.Contains("--with-seed"))
    {
        await app.Services.SeedDatabaseAsync();
    }
    app.UseSwaggerModule();
}

app.UseHttpsRedirection()
    .UseApplicationMiddlewares()
    .UseExceptionMiddleware();

app.Mediate<GetTaskListsQuery>(new(HttpMethods.Get, "/taskList")
{
    Description = "Get all task lists",
    Summary = "This endpoint returns all task lists",
    Tag = "TaskList"
});

app.Mediate<GetSingleTaskListQuery>(new(HttpMethods.Get, "/taskList/{id}")
{
    Description = "Get a single task list",
    Summary = "This endpoint returns a single task list",
    Tag = "TaskList"
});

app.Mediate<CreateTaskListCommand>(new(HttpMethods.Post, "/taskList")
{
    Description = "Create a new task list",
    Summary = "This endpoint creates a new task list",
    Tag = "TaskList"
});

app.Mediate<UpdateTaskListCommand>(new(HttpMethods.Put, "/taskList/{id}")
{
    Description = "Update a task list",
    Summary = "This endpoint updates an existing task list",
    Tag = "TaskList"
});

app.Mediate<DeleteTaskListCommand>(new(HttpMethods.Delete, "/taskList/{id}")
{
    Description = "Delete a task list",
    Summary = "This endpoint deletes an existing task list",
    Tag = "TaskList"
});

app.Mediate<GetTaskListAssigneesQuery>(new(HttpMethods.Get, "/taskList/{taskListId}/assigned")
{
    Description = "Get task list assignees",
    Summary = "This endpoint returns all assignees for a task list",
    Tag = "AssignedTaskList"
});

app.Mediate<CreateAssignedTaskListCommand>(new(HttpMethods.Post, "/taskList/{taskListId}/assigned/{assigneeId}")
{
    Description = "Create a task list assignment",
    Summary = "This endpoint assigns a user to a task list",
    Tag = "AssignedTaskList"
});

app.Mediate<DeleteAssignedTaskListCommand>(new(HttpMethods.Delete, "/taskList/{taskListId}/assigned/{assigneeId}")
{
    Description = "Delete a task list assignment",
    Summary = "This endpoint unassigns a user from a task list",
    Tag = "AssignedTaskList"
});

app.Run();
