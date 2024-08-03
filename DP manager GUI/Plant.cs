using Newtonsoft.Json;
using System;

namespace DP_manager
{
    public class Plant
    {
        [JsonProperty("jaar")]
        public int Jaar { get; set; }

        [JsonProperty("soortCode")]
        public string SoortCode { get; set; }

        [JsonProperty("pm")]
        public UInt16 Pm { get; set; }
    }
}
