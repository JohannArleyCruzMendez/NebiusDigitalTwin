using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Infrastructure.Settings
{


    public class AiProvidersSettings
    {
        public ProviderSetting Nebius { get; set; } = new();
        public ProviderSetting Groq { get; set; } = new();
    }

    public class ProviderSetting
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }

}
