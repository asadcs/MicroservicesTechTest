using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Domain.Exceptions
{
    public class VisitRegistrationException : Exception
    {
        public VisitRegistrationException(string message) : base(message) { }
        public VisitRegistrationException(string message, Exception inner) : base(message, inner) { }
    }
}
