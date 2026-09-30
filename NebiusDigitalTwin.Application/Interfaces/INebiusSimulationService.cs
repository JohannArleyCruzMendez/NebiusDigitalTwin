using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Interfaces
{
    public interface INebiusSimulationService
    {
        // Le enviamos la topología completa (los Nodos de MongoDB) y el ataque que queremos probar (ej. "CVE-2024-3094")
        // Y nos promete devolver el objeto estructurado.
        Task<ReporteSimulacion> SimularAtaqueAsync(IEnumerable<Nodo> topologiaRed, string amenazaObjetivo);
    }
}

