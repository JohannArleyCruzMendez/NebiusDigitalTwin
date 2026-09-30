using MediatR;
using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands
{
  
    // El comando transporta los datos. Implementa IRequest para indicar lo que devuelve (en este caso, el ID del nodo creado).
    public class CrearNodoCommand : IRequest<string>
    {
        public Nodo Nodo { get; set; } = new();
    }
}

