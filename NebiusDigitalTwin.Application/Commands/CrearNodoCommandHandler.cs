using MediatR;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Commands
{
     public class CrearNodoCommandHandler: IRequestHandler<CrearNodoCommand, string>
    {
        private readonly INodoRepository _repository;

        // Inyectamos el repositorio aquí, en la capa de Aplicación.
        public CrearNodoCommandHandler(INodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CrearNodoCommand request, CancellationToken cancellationToken)
        {


            // Mapeamos explícitamente TODOS los campos, incluyendo los anidados
            var nuevoNodo = new Nodo
            {
                Nombre = request.Nodo.Nombre,
                Tipo = request.Nodo.Tipo,
                SistemaOperativo = request.Nodo.SistemaOperativo,
                VersionKernel = request.Nodo.VersionKernel,
                CriticidadActivo = request.Nodo.CriticidadActivo,

                Red = new Red
                {
                    DireccionIp = request.Nodo.Red.DireccionIp,
                    IpPrivada = request.Nodo.Red.IpPrivada, // Nuevo
                    IpPublica = request.Nodo.Red.IpPublica  // Nuevo
                },

                Seguridad = new Seguridad
                {
                    ServiciosExpuestos = request.Nodo.Seguridad.ServiciosExpuestos ?? new List<string>(),
                    Vulnerabilidades = request.Nodo.Seguridad.Vulnerabilidades ?? new List<string>(),
                    PermiteRootSSH = request.Nodo.Seguridad.PermiteRootSSH, // Nuevo
                    AlmacenaDatosSensibles = request.Nodo.Seguridad.AlmacenaDatosSensibles // Nuevo
                },

                ConexionesSalientes = new List<ConexionSaliente>()
            };

            // Asumo que tienes una inyección de tu repositorio así:
            await _repository.CreateAsync(nuevoNodo);
            return nuevoNodo.Id;

        }
    }
}
