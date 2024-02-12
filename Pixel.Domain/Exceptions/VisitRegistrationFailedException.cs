using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Domain.Exceptions
{
    public class VisitRegistrationFailedException : Exception
    {
        public VisitRegistrationFailedException(string message) : base(message) { }
        public VisitRegistrationFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
