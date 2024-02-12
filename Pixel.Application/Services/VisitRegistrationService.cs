using Microsoft.Extensions.Logging;
using Pixel.Application.Contract.Interfaces;
using Pixel.Application.Events;
using Pixel.Domain.Exceptions;
using Serilog;


namespace Pixel.Application.Services
{
    public class VisitRegistrationService : IVisitRegistrationService
    {
        private readonly ILogger<VisitRegistrationService> _logger;

        public VisitRegistrationService(ILogger<VisitRegistrationService> logger)
        {
            _logger = logger;
        }

        public async Task RegisterVisitAsync(VisitRegisteredEvent visitEvent)
        {
            if (visitEvent == null)
            {
                throw new InvalidVisitEventException("Visit event cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(visitEvent.IpAddress))
            {
                throw new InvalidVisitEventException("IP address is required.");
            }

            try
            {
                Log.ForContext("EventContext", "Visit")
                    .Information($"{visitEvent.Timestamp:yyyy-MM-ddTHH:mm:ss.fffZ} | {visitEvent.Referrer} | {visitEvent.UserAgent} | {visitEvent.IpAddress}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                throw new VisitRegistrationFailedException("Failed to register visit.", ex);
            }
        }
    }
}
