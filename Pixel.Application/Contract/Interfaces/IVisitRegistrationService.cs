using Pixel.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Contract.Interfaces
{
    public interface IVisitRegistrationService
    {
        Task RegisterVisitAsync(VisitRegisteredEvent visitEvent);
    }
}
