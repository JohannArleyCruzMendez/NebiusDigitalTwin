using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public interface ICveService
{
    Task<CveResponseDto> SearchAsync(string query); // Cambiado a SearchAsync para coincidir con el controlador
}

public class CveService : ICveService
{
    private readonly HttpClient _httpClient;

    public CveService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DigitalTwinSecurityCenter-HackathonClient");
        }
    }

    public async Task<CveResponseDto> SearchAsync(string query)
    {
        try
        {
            string url = $"https://services.nvd.nist.gov/rest/json/cves/2.0?keywordSearch={Uri.EscapeDataString(query)}";

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            var response = await _httpClient.GetAsync(url, cts.Token);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonString);
                var root = doc.RootElement;

                if (root.TryGetProperty("vulnerabilities", out var vulns) && vulns.GetArrayLength() > 0)
                {
                    var firstVuln = vulns[0].GetProperty("cve");
                    string cveId = firstVuln.GetProperty("id").GetString();

                    string description = "No description available.";
                    if (firstVuln.TryGetProperty("descriptions", out var descs))
                    {
                        foreach (var desc in descs.EnumerateArray())
                        {
                            if (desc.GetProperty("lang").GetString() == "en")
                            {
                                description = desc.GetProperty("value").GetString();
                                break;
                            }
                        }
                    }

                    return new CveResponseDto
                    {
                        CveCode = cveId,
                        Description = description,
                        Source = "NVD-Live"
                    };
                }
            }
        }
        catch
        {
            // Silencia errores de red y pasa al fallback local
        }

        return GetLocalMockFallback(query);
    }

    private CveResponseDto GetLocalMockFallback(string query)
    {
        var p = query.ToLower();
        string code = "CVE-2023-38606";
        string desc = "Apple Kernel state modification vulnerability allowing malicious applications to bypass memory protections.";

        // Uso correcto de .Contains() en lugar de .includes() para C#
        if (p.Contains("log4j") || p.Contains("log4shell"))
        {
            code = "CVE-2021-44228";
            desc = "Apache Log4j2 JNDI Remote Code Execution.";
        }
        else if (p.Contains("apple") || p.Contains("kernel") || p.Contains("38606") || p.Contains("memory"))
        {
            code = "CVE-2023-38606";
            desc = "Apple Kernel state modification vulnerability allowing malicious apps to bypass memory protections.";
        }
        else if (p.Contains("openssl"))
        {
            code = "CVE-2022-3786";
            desc = "OpenSSL X.509 buffer overrun vulnerability.";
        }
        else if (p.Contains("postgres") || p.Contains("sql"))
        {
            code = "CVE-2023-39417";
            desc = "PostgreSQL extension SQL injection vulnerability.";
        }
        else if (p.Contains("redis"))
        {
            code = "CVE-2023-0567";
            desc = "Redis Server Buffer Overflow / Privilege Escalation.";
        }

        return new CveResponseDto
        {
            CveCode = code,
            Description = desc,
            Source = "Local-Fallback"
        };
    }
}