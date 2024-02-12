using MassTransit;
using MediatR;
using Pixel.Application.Events;
using Pixel.Application.Features.Command;
using Pixel.Application.Features.Validators;
using Pixel.Domain.Exceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Features.Handlers
{
    public class AddVisitCommandHandler : IRequestHandler<AddVisitCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IAddVisitCommandValidator _validator;

        public AddVisitCommandHandler(IPublishEndpoint publishEndpoint, IAddVisitCommandValidator validator)
        {
            _publishEndpoint = publishEndpoint;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddVisitCommand request, CancellationToken cancellationToken)
        {
            _validator.Validate(request);

            try
            {
                await _publishEndpoint.Publish(new VisitRegisteredEvent
                {
                    Referrer = request.Referrer,
                    UserAgent = request.UserAgent,
                    IpAddress = request.IpAddress,
                    Timestamp = DateTime.UtcNow
                }, cancellationToken);

                Log.Information("VisitRegisteredEvent published successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while publishing the VisitRegisteredEvent.");
                throw new VisitRegistrationException("Failed to publish VisitRegisteredEvent.", ex);
            }

            return Unit.Value;
        }
    }
}
