using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Domain.Entities
{
     public class ConexionSaliente
    {
        public string NodoDestinoId { get; set; } = string.Empty;
        public int Puerto { get; set; }
        public string Protocolo { get; set; } = string.Empty;
        public string ServicioDestino { get; set; } = string.Empty;

    
    }
}
