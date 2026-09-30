using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands
{
    
    // Devuelve un booleano(true si se borró, false si no existía)
    public class EliminarNodoCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }
}
