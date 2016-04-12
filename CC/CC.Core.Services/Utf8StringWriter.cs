using System.IO;
using System.Text;

namespace CC.Core.Services
{
    /// <summary>
    /// Реализует <see cref="TextWriter"/> для записи данных в строку в формате UTF-8.
    /// </summary>
    internal sealed class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Возвращает кодировку данных.
        /// </summary>
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
