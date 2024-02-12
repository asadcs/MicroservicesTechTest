using Serilog;
using MassTransit;
using System.Text;
using Serilog.Filters;
using Pixel.Application.Events;
using Pixel.Application.Contract.Interfaces;
using Pixel.Infrastructure.Messaging;
using Pixel.Application.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Configuration.AddJsonFile($"storageService.appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"storageService.appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Configure Serilog for logging
var visitsLogPath = builder.Configuration.GetValue<string>("Logging:VisitsLogPath") ?? "logs/visits.log";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetValue<string>("Logging:FilePath") ?? "logs/activitylog.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.WithProperty("EventContext", "Visit"))
        .WriteTo.File(visitsLogPath, outputTemplate: "{Message}{NewLine}"))
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection for services
builder.Services.AddTransient<IVisitRegistrationService, VisitRegistrationService>();

// Add MassTransit with RabbitMQ
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<VisitRegisteredEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMQSettings.GetValue<string>("Host"), rabbitMQSettings.GetValue<string>("VirtualHost"), h =>
        {
            h.Username(rabbitMQSettings.GetValue<string>("Username"));
            h.Password(rabbitMQSettings.GetValue<string>("Password"));
        });

        cfg.ReceiveEndpoint("visit-registered-event-queue", e =>
        {
            e.ConfigureConsumer<VisitRegisteredEventConsumer>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


