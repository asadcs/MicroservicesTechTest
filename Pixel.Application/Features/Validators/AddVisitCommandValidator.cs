using Pixel.Application.Features.Command;
using Pixel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Features.Validators
{
    public class AddVisitCommandValidator : IAddVisitCommandValidator
    {
        public void Validate(AddVisitCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.IpAddress))
                throw new CommandProcessingException("IP address is required and cannot be empty.");
        }
    }
}
