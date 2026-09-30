using MediatR;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands;

public class SimularAmenazaCommandHandler : IRequestHandler<SimularAmenazaCommand, ReporteSimulacion>
{
    private readonly INodoRepository _repository;
    private readonly INebiusSimulationService _simulationService;

    public SimularAmenazaCommandHandler(
        INodoRepository repository,
        INebiusSimulationService simulationService)
    {
        _repository = repository;
        _simulationService = simulationService;
    }

    public async Task<ReporteSimulacion> Handle(SimularAmenazaCommand request, CancellationToken cancellationToken)
    {
        var topologiaRed = await _repository.GetAllAsync();
        return await _simulationService.SimularAtaqueAsync(topologiaRed, request.AmenazaObjetivo);
    }
}
