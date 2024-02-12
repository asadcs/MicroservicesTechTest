using MassTransit;
using Microsoft.Extensions.Logging;
using Pixel.Application.Contract.Interfaces;
using Pixel.Application.Events;
using Pixel.Domain.Exceptions;


namespace Pixel.Infrastructure.Messaging
{
    public class VisitRegisteredEventConsumer : IConsumer<VisitRegisteredEvent>
    {
        private readonly ILogger<VisitRegisteredEventConsumer> _logger;
        private readonly IVisitRegistrationService _registrationService;

        public VisitRegisteredEventConsumer(ILogger<VisitRegisteredEventConsumer> logger, IVisitRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public async Task Consume(ConsumeContext<VisitRegisteredEvent> context)
        {
            try
            {
                if (context.Message == null)
                {
                    _logger.LogError("Received a null VisitRegisteredEvent message.");
                    return;
                }

                await _registrationService.RegisterVisitAsync(context.Message);
            }
            catch (InvalidVisitEventException ivex)
            {
                _logger.LogError(ivex, "Invalid visit event received.");
            }
            catch (VisitRegistrationFailedException vrfex)
            {
                _logger.LogError(vrfex, "Visit registration failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error processing VisitRegisteredEvent.");
            }
        }
    }

}
