using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FirstFloor.ModernUI.Presentation;

namespace Crossword_Application_Modern.Themes
{
    static class UserBackgroundFileExist
    {
        private static string filePath = "C:/1.jpg";

        /// <summary>
        /// Возвращает значение, существует ли пользовательский файл фона C:\1.jpg.
        /// </summary>
        /// <returns>true - существует, false - не существует.</returns>
        public static bool CheckBackgroundFileExist()
        { return File.Exists(filePath); }
    }
}
