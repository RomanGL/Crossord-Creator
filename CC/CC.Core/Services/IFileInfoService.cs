using CC.Core.Model;

namespace CC.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы с версией файла Crossword Creator.
    /// </summary>
    public interface IFileInfoService
    {
        /// <summary>
        /// Возвращает версию файла Crossword Creator.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        CCVersion GetFileInfo(IFile file, IFileService service);
    }
}
