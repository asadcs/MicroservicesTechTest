using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Application.Events
{
    public class VisitRegisteredEvent
    {
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
