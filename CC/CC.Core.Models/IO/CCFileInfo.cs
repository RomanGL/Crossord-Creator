using Newtonsoft.Json;

namespace CC.Core.Models.IO
{
    public sealed class CCFileInfo : IFile
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Path { get; set; }

        [JsonProperty("Type")]
        public CCFileType Type { get; set; }

        [JsonProperty("AppVersion")]
        public CCVersion Version { get; set; }
    }
}
