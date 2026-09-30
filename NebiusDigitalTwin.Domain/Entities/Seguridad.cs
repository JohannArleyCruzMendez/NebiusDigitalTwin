using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Domain.Entities
{
     public class Seguridad
    {
        public DateTimeOffset UltimaActualizacion { get; set; }
        public bool PermiteRootSSH { get; set; }
        public bool AlmacenaDatosSensibles { get; set; }


        public List<string> ServiciosExpuestos { get; set; } = new();

        // ¡Esta es la propiedad nueva que necesitamos para recibir los datos del frontend!
        public List<string> Vulnerabilidades { get; set; } = new();


    }
}
