using MediatR;
using NebiusDigitalTwin.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands
{
    public class EliminarNodoCommandHandler : IRequestHandler<EliminarNodoCommand, bool>
    {
        private readonly INodoRepository _repository;

        public EliminarNodoCommandHandler(INodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(EliminarNodoCommand request, CancellationToken cancellationToken)
        {

            return await _repository.DeleteAsync(request.Id);
        }
    }
}
