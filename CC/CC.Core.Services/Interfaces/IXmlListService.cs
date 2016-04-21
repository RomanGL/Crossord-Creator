using CC.Core.Models;
using CC.Core.Models.IO;
using System.Collections.Generic;

namespace CC.Core.Services.Interfaces
{
    /// <summary>
    /// Представляет сервис для работы со списком терминов (в формате Crossword Creator 1.x).
    /// </summary>
    public interface IXmlListService
    {
        /// <summary>
        /// Возвращает список терминов из текстовой строки.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        List<ListWord> GetListFromFile(IFile file);

        /// <summary>
        /// Сохраняет список терминов в файл.
        /// </summary>
        /// <param name="list">Список для сохранения.</param>
        /// <param name="version">Версия приложения, создавшего список.</param>
        /// <param name="file">Файл, в который требуется записать список.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности сериализовать 
        /// список или записать данные в файл.</exception>
        void SaveListFile(IEnumerable<ListWord> list, CCVersion version, IFile file);
    }
}
