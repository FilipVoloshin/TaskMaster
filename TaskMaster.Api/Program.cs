using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Extensions;
using TaskMaster.Application.Extensions;
using TaskMaster.Application.MediatR.AssignedTaskLists.Command;
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
    //await app.Services.SeedDatabaseAsync();
    app.UseSwaggerModule();
}

app.UseHttpsRedirection()
    .UseApplicationMiddlewares()
    .UseExceptionMiddleware();

#region Task List routes
app.MapGet("/taskList", async ([AsParameters] GetTaskListsQuery query,
    [FromServices] IMediator mediator, IValidator<GetTaskListsQuery> validator) =>
{
    var validationResult = await validator.ValidateAsync(query);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var taskLists = await mediator.Send(query);
    return Results.Ok(taskLists);
})
.WithName("GetTaskLists")
.WithOpenApi();

app.MapGet("/taskList/{id}", async ([AsParameters] GetSingleTaskListQuery query, [FromServices] IMediator mediator) =>
{
    var taskList = await mediator.Send(query);
    return Results.Ok(taskList);
})
.WithName("GetSingleTaskList")
.WithOpenApi();

app.MapPost("/taskList", async ([FromBody] CreateTaskListCommand request,
    [FromServices] IMediator mediator, IValidator<CreateTaskListCommand> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var taskList = await mediator.Send(request);
    return Results.Ok(taskList);
})
.WithName("CreateTaskList")
.WithOpenApi();

app.MapPut("/taskList/{id}", async ([AsParameters] UpdateTaskListCommand request,
    [FromServices] IMediator mediator, IValidator<UpdateTaskListCommand> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var taskList = await mediator.Send(request);
    return Results.Ok(taskList);
})
.WithName("UpdateTaskList")
.WithOpenApi();

app.MapDelete("/taskList/{id}", async ([AsParameters] DeleteTaskListCommand request, [FromServices] IMediator mediator) =>
{
    await mediator.Send(request);
    return Results.Ok();
})
.WithName("DeleteTaskList")
.WithOpenApi();
#endregion

#region Assigned Task list routes

app.MapPost("/taskList/{taskListId}/assigned/{assigneeId}", async ([AsParameters] CreateAssignedTaskListCommand request,
    [FromServices] IMediator mediator, IValidator<CreateAssignedTaskListCommand> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var taskList = await mediator.Send(request);
    return Results.Ok(taskList);
})
.WithName("CreateTaskListAssignment")
.WithOpenApi();

app.MapDelete("/taskList/{taskListId}/assigned/{assigneeId}", async ([AsParameters] DeleteAssignedTaskListCommand request, 
    [FromServices] IMediator mediator, IValidator<DeleteAssignedTaskListCommand> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    await mediator.Send(request);
    return Results.Ok();
})
.WithName("DeleteTaskListAssignment")
.WithOpenApi();

#endregion

app.Run();
