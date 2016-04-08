using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, предназначенный для работы с папкой приложения в AppData.
    /// </summary>
    public static class AppLocalDirectory
    {
        /// <summary>
        /// Путь до папки приложения в AppData.
        /// </summary>
        private static string applicationLocalDirectory = 
            String.Format("{0}\\CCApp\\", 
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        /// <summary>
        /// Создает папку приложения в AppData.
        /// </summary>
        public static void CreateApplicationDirectory()
        {
            if (CheckAplicationDirectory() == false)
            { Directory.CreateDirectory(applicationLocalDirectory); }
        }

        /// <summary>
        /// Проверяет наличие папки приложения в AppData.
        /// </summary>
        /// <returns>Значение, указывающее на наличие или отсутствие папки.</returns>
        private static bool CheckAplicationDirectory()
        { return Directory.Exists(applicationLocalDirectory); }

        /// <summary>
        /// Возвращает путь до папки приложения в AppData.
        /// </summary>
        public static string GetApplicationLocalDirectory
        { get { return applicationLocalDirectory; } }
    }
}
