using CC.Core.Models;
using CC.Core.Models.IO;
using System.IO;

namespace CC.Core.Services.Interfaces
{
    /// <summary>
    /// Представляет сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Считывает текст из файла и возвращает его.
        /// </summary>
        /// <param name="file">Файл.</param>
        TextReader ReadText(IFile file);

        /// <summary>
        /// Записывает текст в файл и возвращает значение, успешно ли завршилась запись.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="text">Текст для записи.</param>
        void WriteText(IFile file, string text);        
    }
}
