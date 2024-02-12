using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Domain.Exceptions
{
    public class CommandProcessingException : Exception
    {
        public CommandProcessingException(string message) : base(message) { }
        public CommandProcessingException(string message, Exception inner) : base(message, inner) { }
    }
}
