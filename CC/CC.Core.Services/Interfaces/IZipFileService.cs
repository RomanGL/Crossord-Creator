using CC.Core.Models;
using CC.Core.Models.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.Services.Interfaces
{
    /// <summary>
    /// Представляет сервис для работы с Zip-файлами Crossword Creator.
    /// </summary>
    public interface IZipFileService
    {
        CCFileInfo GetFileInfo(IFile file);
    }
}
