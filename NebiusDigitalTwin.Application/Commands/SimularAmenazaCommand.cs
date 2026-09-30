using MediatR;
using NebiusDigitalTwin.Domain.Entities;

namespace NebiusDigitalTwin.Application.Commands;

public class SimularAmenazaCommand : IRequest<ReporteSimulacion>
{
    public string AmenazaObjetivo { get; set; } = string.Empty;
}