using System.IO;

namespace CC.Core.Model
{
    /// <summary>
    /// Представляет файл.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Имя файла с расширением.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Абсолютный путь к файлу.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Возвращает поток произвольного доступа к файлу.
        /// </summary>
        Stream RandomAccessStream { get; set; }
    }
}
