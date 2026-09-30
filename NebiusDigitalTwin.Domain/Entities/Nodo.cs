using System;
using System.Collections.Generic;

namespace NebiusDigitalTwin.Domain.Entities
{
    public class Nodo
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string SistemaOperativo { get; set; } = string.Empty;
        public string VersionKernel { get; set; } = string.Empty;
        public string CriticidadActivo { get; set; } = string.Empty;

        // Subdocumentos
        public Red Red { get; set; } = new();
        public Seguridad Seguridad { get; set; } = new();

        // Referencias a otros nodos
        public List<ConexionSaliente> ConexionesSalientes { get; set; } = new();
    }

   
}
