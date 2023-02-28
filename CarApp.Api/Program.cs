using CarApp.Application;
using CarApp.Core.Common;
using CarApp.Core.Domain;
using CarApp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarContext>(opt => opt.UseInMemoryDatabase("CarList"));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetCarCommand).Assembly);
});
var app = builder.Build();

app.MapGet("/car/{id}", async (Guid id, IMediator mediator) =>
{
    var car = await mediator.Send(new GetCarCommand
    {
        Id = id
    });
    return Results.Ok(car);
});

app.Run();

public partial class Program { }