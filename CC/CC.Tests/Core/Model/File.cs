using CC.Core.Model;
using System.IO;

namespace CC.Tests.Core.Model
{
    /// <summary>
    /// Представляет файл.
    /// </summary>
    public sealed class File : IFile
    {
        /// <summary>
        /// Название файла с расширением.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полный путь к файлу.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Возвращает поток произвольного доступа к файлу.
        /// </summary>
        public Stream RandomAccessStream { get; set; }
    }
}
