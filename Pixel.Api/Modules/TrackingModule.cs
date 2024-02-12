using Carter;
using MassTransit.Mediator;
using Pixel.Application.Features.Command;
using Serilog;

namespace Pixel.Api.Modules
{
    public class TrackingModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/track", async (HttpContext context, IMediator mediator) =>
            {
                var command = new AddVisitCommand(
                    context.Request.Headers["Referer"].FirstOrDefault(),
                    context.Request.Headers["User-Agent"].FirstOrDefault(),
                    context.Connection.RemoteIpAddress?.ToString());

                try
                {
                    await mediator.Send(command);
                    var pixel = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
                    return Results.File(pixel, "image/gif");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while processing the command.");
                    return Results.Problem("An error occurred while processing your request.");
                }
            });

        }
    }
}
