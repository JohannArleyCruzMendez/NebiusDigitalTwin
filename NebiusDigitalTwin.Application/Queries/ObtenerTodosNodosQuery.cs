using MediatR;
using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Queries
{
    // La consulta no requiere que le enviemos datos (por eso la clase está vacía), 
    // pero en IRequest le indicamos que debe devolver una lista de Nodos.
    public class ObtenerTodosNodosQuery : IRequest<IEnumerable<Nodo>>
    {
    }
}
