using MediatR;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands
{
    public class VincularNodosCommandHandler : IRequestHandler<VincularNodosCommand, bool>
    {
        private readonly INodoRepository _repository;

        public VincularNodosCommandHandler(INodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(VincularNodosCommand request, CancellationToken cancellationToken)
        {
            var origen = await _repository.GetByIdAsync(request.NodoOrigenId);
            var destino = await _repository.GetByIdAsync(request.NodoDestinoId);

            if (origen == null || destino == null) return false;

            // Evitamos duplicar la misma conexión exacta
            if (!origen.ConexionesSalientes.Any(c => c.NodoDestinoId == request.NodoDestinoId))
            {
                origen.ConexionesSalientes.Add(new ConexionSaliente
                {
                    NodoDestinoId = request.NodoDestinoId,
                    Protocolo = request.Protocolo,
                    Puerto = request.Puerto
                });

                await _repository.UpdateAsync(origen);
            }

            return true;
        }
    }
}