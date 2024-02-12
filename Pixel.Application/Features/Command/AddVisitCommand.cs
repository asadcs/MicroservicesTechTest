using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Features.Command
{
    public record AddVisitCommand(string Referrer, string UserAgent, string IpAddress) : IRequest<Unit>;
}
