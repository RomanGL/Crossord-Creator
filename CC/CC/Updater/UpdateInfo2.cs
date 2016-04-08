using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Представляет информацию об обновлении CC версии выше 2.0.0.0.
    /// </summary>
    public sealed class UpdateInfo2
    {
        public const string UpdateInfoUrl = "http://crosswordcreator.esy.es/app/update2.txt";

        [JsonProperty("new_version")]
        public string NewVersion { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("framework")]
        public int FrameworkVersion { get; set; }

        [JsonProperty("changes")]
        public List<string> Changes { get; set; }
    }
}
