using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CC.Core.Services.Interfaces;

namespace CC.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы со списками терминов в формате JSON.
    /// </summary>
    public sealed class JsonListService : IListService, IFileInfoService
    {
        /// <summary>
        /// Возвращает версию файла Crossword Creator.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        public CCVersion GetFileInfo(IFile file, IFileService service)
        {
            try
            {
                using (var reader = new JsonTextReader(service.ReadText(file)))
                {
                    var obj = JToken.ReadFrom(reader);
                    var version = obj["AppVersion"].ToObject<CCVersion>();
                    return version;
                }                 
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        /// <summary>
        /// Возвращает список терминов из текстовой строки.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        public List<ListWord> GetListFromFile(IFile file, IFileService service)
        {
            try
            {
                using (var reader = new JsonTextReader(service.ReadText(file)))
                {
                    var obj = JToken.ReadFrom(reader);
                    var list = obj["Content"].ToObject<List<ListWord>>();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        /// <summary>
        /// Сохраняет список терминов в файл.
        /// </summary>
        /// <param name="list">Список для сохранения.</param>
        /// <param name="version">Версия приложения, создавшего список.</param>
        /// <param name="file">Файл, в который требуется записать список.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности сериализовать 
        /// список или записать данные в файл.</exception>
        public void SaveListFile(IEnumerable<ListWord> list, CCVersion version, IFile file, IFileService service)
        {
            try
            {                
                var resultFile = new CCListFile
                {
                    AppVersion = version,
                    Content = list.ToList()
                };
                string text = JsonConvert.SerializeObject(resultFile);
                service.WriteText(file, text);
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось сохранить файл.", ex);
            }
        }
    }
}
