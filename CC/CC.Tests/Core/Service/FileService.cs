using CC.Core.Model;
using CC.Core.Services;
using System.IO;
using System.Text;

namespace CC.Tests.Core.Service
{
    /// <summary>
    /// Представляет сервис для работы с файлами.
    /// </summary>
    public sealed class FileService : IFileService
    {
        /// <summary>
        /// Считывает текст из файла и возвращает его.
        /// </summary>
        /// <param name="file">Файл.</param>
        public TextReader ReadText(IFile file)
        {
            return File.OpenText(file.Path);
        }

        /// <summary>
        /// Записывает текст в файл и возвращает значение, успешно ли завершилась запись.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="text">Текст для записи.</param>
        public void WriteText(IFile file, string text)
        {
            File.WriteAllText(file.Path, text, Encoding.UTF8);
        }
    }
}
