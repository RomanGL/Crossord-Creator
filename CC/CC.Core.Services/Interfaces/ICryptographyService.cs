using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.Services.Interfaces
{
    /// <summary>
    /// Представляет сервис криптографии Crossword Creator.
    /// </summary>
    public interface ICryptographyService
    {
        string GetSHA1(string input);
    }
}
