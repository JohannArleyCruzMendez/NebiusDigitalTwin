using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Domain.Entities
{
     public class Red
    {
        public string IpPrivada { get; set; } = string.Empty;
        public string IpPublica { get; set; } = string.Empty;
        public List<PuertoAbierto> PuertosAbiertos { get; set; } = new();
        public string DireccionIp { get; set; } = string.Empty;
    }
}
