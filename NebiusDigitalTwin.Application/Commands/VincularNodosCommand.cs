using MediatR;

namespace NebiusDigitalTwin.Application.Commands
{
    public class VincularNodosCommand : IRequest<bool>
    {
        public string NodoOrigenId { get; set; } = string.Empty;
        public string NodoDestinoId { get; set; } = string.Empty;
        public string Protocolo { get; set; } = "TCP";
        public int Puerto { get; set; }
    }
}