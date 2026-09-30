using MediatR;
using Microsoft.AspNetCore.Mvc;
using NebiusDigitalTwin.Application.Commands;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Application.Queries;
using NebiusDigitalTwin.Domain.Entities;

namespace NebiusDigitalTwin.API.Controllers // <-- Corregida la L extra
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        // CAMBIO CLAVE: Ahora recibimos el Comando directamente
        public async Task<IActionResult> CrearNodo([FromBody] CrearNodoCommand command)
        {
            // MediatR se encarga de buscar el Handler correcto y ejecutarlo
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(ObtenerTodos), new { id = id }, command.Nodo);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            // Instanciamos la consulta
            var query = new ObtenerTodosNodosQuery();

            // MediatR busca el Handler, ejecuta la lectura y nos devuelve la lista
            var nodos = await _mediator.Send(query);

            return Ok(nodos);
        }

        [HttpPost("simular")]
        public async Task<IActionResult> SimularAtaque([FromBody] SimularAmenazaCommand command)
        {
            var reporte = await _mediator.Send(command);
            return Ok(reporte);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarNodo(string id)
        {
            var command = new EliminarNodoCommand { Id = id };
            var resultado = await _mediator.Send(command);

            if (!resultado)
            {
                return NotFound(new { message = "Asset not found." });
            }

            return Ok(new { message = "Asset deleted successfully." });
        }

        [HttpPost("vincular")]
        public async Task<IActionResult> VincularNodos([FromBody] VincularNodosCommand command)
        {
            var resultado = await _mediator.Send(command);
            if (!resultado)
            {
                return BadRequest(new { message = "Could not link nodes. Verify IDs." });
            }

            return Ok(new { message = "Nodes linked successfully." });
        }


    }
}