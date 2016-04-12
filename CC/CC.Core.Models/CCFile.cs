using System;
using System.IO;

namespace CC.Core.Models
{
    /// <summary>
    /// Представляет файл Crossword Creator.
    /// Это абстрактный класс.
    /// </summary>
    /// <typeparam name="T">Тип содержимого файла.</typeparam>
    public abstract class CCFile<T> : IFile
    {
        /// <summary>
        /// Версия приложения, создавшего файл.
        /// </summary>
        public CCVersion AppVersion { get; set; }

        /// <summary>
        /// Содержимое файла.
        /// </summary>
        public T Content { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Stream RandomAccessStream { get; set; }
    }
}
