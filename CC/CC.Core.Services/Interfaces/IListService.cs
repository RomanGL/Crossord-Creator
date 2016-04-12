using CC.Core.Models;
using System.Collections.Generic;

namespace CC.Core.Services.Interfaces
{
    /// <summary>
    /// Представляет сервис для работы со списком терминов.
    /// </summary>
    public interface IListService
    {
        /// <summary>
        /// Возвращает список терминов из текстовой строки.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        List<ListWord> GetListFromFile(IFile file, IFileService service);

        /// <summary>
        /// Сохраняет список терминов в файл.
        /// </summary>
        /// <param name="list">Список для сохранения.</param>
        /// <param name="version">Версия приложения, создавшего список.</param>
        /// <param name="file">Файл, в который требуется записать список.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности сериализовать 
        /// список или записать данные в файл.</exception>
        void SaveListFile(IEnumerable<ListWord> list, CCVersion version, IFile file, IFileService service);
    }
}
