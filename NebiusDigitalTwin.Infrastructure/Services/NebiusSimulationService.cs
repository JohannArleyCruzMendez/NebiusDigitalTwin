using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using NebiusDigitalTwin.Infrastructure.Settings;

namespace NebiusDigitalTwin.Infrastructure.Services;

public class NebiusSimulationService : INebiusSimulationService
{
    private readonly HttpClient _httpClient;
    private readonly AiProvidersSettings _aiSettings;
    private readonly ILogger<NebiusSimulationService> _logger;

    public NebiusSimulationService(
        HttpClient httpClient,
        IOptions<AiProvidersSettings> aiSettings,
        ILogger<NebiusSimulationService> logger)
    {
        _httpClient = httpClient;
        _aiSettings = aiSettings.Value;
        _logger = logger;
    }

    public async Task<ReporteSimulacion> SimularAtaqueAsync(IEnumerable<Nodo> topologiaRed, string amenazaObjetivo)
    {
        var topologiaJson = JsonSerializer.Serialize(topologiaRed);

        var systemPrompt = @"You are a cybersecurity expert analyzing a Digital Twin network. 
You MUST provide your response entirely in English.
Analyze the following threat against the provided network nodes and return the result STRICTLY in this JSON format:
{
    ""nivelRiesgoGlobal"": ""Critical/High/Medium/Low"",
    ""resumenEjecutivo"": ""Brief summary in English"",
    ""rutasAtaquePosibles"": [""Path 1 in English"", ""Path 2 in English""],
    ""mitigacionesRecomendadas"": [""Mitigation 1 in English"", ""Mitigation 2 in English""]
}";

        var userPrompt = $"Amenaza a simular: {amenazaObjetivo}\nTopología de Red:\n{topologiaJson}";

        var payload = new
        {
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            temperature = 0.2,
            max_tokens = 800
        };

        // 1. INTENTO PRINCIPAL: Nebius Token Factory
        try
        {
            _logger.LogInformation("Enviando petición de simulación a Nebius Token Factory...");
            var reporte = await EnviarPeticionAsync(_aiSettings.Nebius, payload);
            reporte.ProveedorUtilizado = "Nebius Token Factory (Active)";
            return reporte;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Nebius falló o no está disponible. Conmutando automáticamente al respaldo de Groq...");

            // 2. RESPALDO AUTOMÁTICO: Groq
            try
            {
                var reporteGroq = await EnviarPeticionAsync(_aiSettings.Groq, payload);
                reporteGroq.ProveedorUtilizado = "Groq AI (Fallback Active)";
                return reporteGroq;
            }
            catch (Exception groqEx)
            {
                _logger.LogError(groqEx, "El proveedor de respaldo Groq también falló.");
                throw new Exception("Ambos proveedores de IA (Nebius y Groq) fallaron al procesar la simulación.", groqEx);
            }
        }
    }

    private async Task<ReporteSimulacion> EnviarPeticionAsync(ProviderSetting provider, object payloadObj)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(provider.BaseUrl), "chat/completions"))
        {
            Content = JsonContent.Create(new
            {
                model = provider.Model,
                messages = ((dynamic)payloadObj).messages,
                temperature = ((dynamic)payloadObj).temperature
            })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetalle = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error del proveedor ({response.StatusCode}): {errorDetalle}");
        }

        var responseBody = await response.Content.ReadFromJsonAsync<JsonElement>();
        var contentString = responseBody
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        var reporte = JsonSerializer.Deserialize<ReporteSimulacion>(contentString!, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return reporte ?? new ReporteSimulacion();
    }
}