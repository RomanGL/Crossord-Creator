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
        string OpenListFile();

        string OpenGridFile();       
    }
}
