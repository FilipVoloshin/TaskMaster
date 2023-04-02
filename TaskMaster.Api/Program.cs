using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Extensions;
using TaskMaster.Application.Extensions;
using TaskMaster.Application.MediatR.TaskLists.Queries;
using TaskMaster.Infrastructure.Extensions;
using TaskMaster.Swagger.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerModule()
                .RegisterInfrastructure(builder.Configuration)
                .RegisterApplicationServices()
                .RegisterMediatR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //await app.Services.SeedDatabaseAsync();
    app.UseSwaggerModule();
}

app.UseHttpsRedirection()
   .UseApplicationMiddlewares()
   .UseExceptionMiddleware();

app.MapGet("/taskList/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
{
    var taskList = await mediator.Send(new GetSingleTaskListQuery(id));
    return Results.Ok(taskList);
})
.WithName("GetSingleTaskList")
.WithOpenApi();

app.Run();
