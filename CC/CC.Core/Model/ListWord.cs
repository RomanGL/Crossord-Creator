using Newtonsoft.Json;

namespace CC.Core.Model
{
    /// <summary>
    /// Представляет слово из списка терминов.
    /// </summary>
    public class ListWord
    {
        private string _answer;

        /// <summary>
        /// Идентификатор терминов.
        /// </summary>
        [JsonProperty("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Термин.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer
        {
            get { return _answer; }
            set { _answer = value.ToLower(); }
        }

        /// <summary>
        /// Вопрос к термину.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}
