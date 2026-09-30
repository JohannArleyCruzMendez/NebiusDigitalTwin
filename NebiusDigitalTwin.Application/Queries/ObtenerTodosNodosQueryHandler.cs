using MediatR;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Queries
{
    public class ObtenerTodosNodosQueryHandler : IRequestHandler<ObtenerTodosNodosQuery, IEnumerable<Nodo>>
    {
        private readonly INodoRepository _repository;

        public ObtenerTodosNodosQueryHandler(INodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Nodo>> Handle(ObtenerTodosNodosQuery request, CancellationToken cancellationToken)
        {
            // Ejecutamos la lectura en MongoDB
            return await _repository.GetAllAsync();
        }
    }
}
