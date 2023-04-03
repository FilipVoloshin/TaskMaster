using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Extensions;
using TaskMaster.Application.Extensions;
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

app.MapGet("/taskList/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
{
    var taskList = await mediator.Send(new GetSingleTaskListQuery(id));
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

app.MapPut("/taskList/{id}", async ([FromRoute] Guid id, [FromBody] UpdateTaskListCommand request,
    [FromServices] IMediator mediator, IValidator<UpdateTaskListCommand> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    request.Id = id;

    var taskList = await mediator.Send(request);
    return Results.Ok(taskList);
})
.WithName("UpdateTaskList")
.WithOpenApi();

app.MapDelete("/taskList/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
{
    await mediator.Send(new DeleteTaskListCommand(id));
    return Results.Ok();
})
.WithName("DeleteTaskList")
.WithOpenApi();

app.Run();
