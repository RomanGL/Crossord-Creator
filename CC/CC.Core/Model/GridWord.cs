using Newtonsoft.Json;

namespace CC.Core.Model
{
    /// <summary>
    /// Представляет слово на сетке кроссворда.
    /// </summary>
    public class GridWord : ListWord
    {
        /// <summary>
        /// Ориентация слова на сетке.
        /// </summary>
        [JsonProperty("orientation")]
        public GridWordOrientation Orientation { get; set; }

        /// <summary>
        /// Положение слова по горизонтали.
        /// </summary>
        [JsonProperty("X")]
        public int X { get; set; }

        /// <summary>
        /// Ориентация слова по вертикали.
        /// </summary>
        [JsonProperty("Y")]
        public int Y { get; set; }
    }
}
