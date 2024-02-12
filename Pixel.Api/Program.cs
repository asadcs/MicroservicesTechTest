using Carter;
using MediatR;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Pixel.Application.Features.Validators;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Configuration.AddJsonFile($"Pixel.Api.appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"Pixel.Api.appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Logging:FilePath"] ?? "logs/activitylog.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
        cfg.Host(rabbitMQSettings["Host"], rabbitMQSettings["VirtualHost"], h =>
        {
            h.Username(rabbitMQSettings["Username"]);
            h.Password(rabbitMQSettings["Password"]);
        });
    });
});
builder.Services.AddSingleton<IAddVisitCommandValidator, AddVisitCommandValidator>();
builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.Run();
















