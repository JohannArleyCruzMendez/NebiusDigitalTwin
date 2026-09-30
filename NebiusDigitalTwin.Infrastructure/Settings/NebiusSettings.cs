using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Infrastructure.Settings
{
    public class NebiusSettings
    {
        public string BaseUrl { get; set; } = String.Empty;
        public string ApiKey { get; set; } = string.Empty;

        public string Model {  get; set; } = String.Empty;

    }
}
