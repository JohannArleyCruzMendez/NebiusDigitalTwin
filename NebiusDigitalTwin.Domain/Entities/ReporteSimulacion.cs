using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Domain.Entities
{
    public class ReporteSimulacion
    {

        // Ej: "Crítico", "Alto", "Medio", "Bajo"
        public string NivelRiesgoGlobal { get; set; } = string.Empty;

        // Una breve explicación de por qué asignó ese riesgo
        public string ResumenEjecutivo { get; set; } = string.Empty;

        // Lista de pasos que tomaría el atacante basándose en tu mapa de red
        public List<string> RutasAtaquePosibles { get; set; } = new();

        // Recomendaciones específicas para parchar o configurar los Nodos
        public List<string> MitigacionesRecomendadas { get; set; } = new();

        public string ProveedorUtilizado { get; set; } = "Nebius Token Factory"; // Para indicar qué IA respondió
    }
}
