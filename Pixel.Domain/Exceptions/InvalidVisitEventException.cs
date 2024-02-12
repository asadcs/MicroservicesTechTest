using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Domain.Exceptions
{
    public class InvalidVisitEventException : Exception
    {
        public InvalidVisitEventException(string message) : base(message) { }
        public InvalidVisitEventException(string message, Exception inner) : base(message, inner) { }
    }
}
