using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace NebiusDigitalTwin.Web.Controllers;

public class NodosController : Controller
   {


    private readonly ICveService _cveService;

    private readonly IHttpClientFactory _httpClientFactory;

    public NodosController(IHttpClientFactory httpClientFactory,ICveService cveService)
    {
        _httpClientFactory = httpClientFactory;
        _cveService = cveService;
    }

    // 1. Mostrar la lista de Nodos y el formulario para agregar
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("DigitalTwinApi");
        var response = await client.GetAsync("Nodos");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            // Aquí deserializarías a una lista de Nodos (puedes crear un DTO/ViewModel)
            ViewBag.NodosJSON = content;
        }

        return View();
    }



    [HttpPost("consultar-cve")]
    public async Task<IActionResult> ConsultarCve([FromBody] CveQueryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Query))
        {
            return BadRequest(new { message = "Query cannot be empty." });
        }

        var result = await _cveService.SearchAsync(request.Query);
        return Ok(result);
    }





    // 2. Vista del Dashboard de Simulación
    public IActionResult Simulacion()
    {
        return View();
    }


    [HttpGet]
    public IActionResult NetworkMap()
    {
        return View();
    }
}
