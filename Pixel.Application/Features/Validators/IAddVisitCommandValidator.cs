using Pixel.Application.Features.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Features.Validators
{
    public interface IAddVisitCommandValidator
    {
        void Validate(AddVisitCommand command);
    }
}
