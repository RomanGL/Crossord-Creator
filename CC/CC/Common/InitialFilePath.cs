using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, обрабатывающий путь файла, полученный при запуске приложения.
    /// </summary>
    static class InitialFilePath
    {
        private static string filePath;
        private static string fileExtension;
        private static bool wasHandled = false;

        /// <summary>
        /// Подготавливает приложение к открытию файла, полученного призапуске приложения.
        /// </summary>
        public static void Prepare()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                filePath = args[1];
                fileExtension = Path.GetExtension(filePath);

                if (GetFileExtension == FileExtension.CrosswordGridFile)
                {
                    MessageBoxResult result = Messages.SelectOperationWithLoadedGridFile();
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow mainWindow = ((MainWindow)App.Current.MainWindow);
                        mainWindow.ContentSource = new Uri("/Pages/CrosswordViewer.xaml", UriKind.Relative);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        MainWindow mainWindow = ((MainWindow)App.Current.MainWindow);
                        mainWindow.ContentSource = new Uri("/Pages/CrosswordEditor.xaml", UriKind.Relative);
                    }
                    else
                    { 
                        Handled();
                        App.Current.Shutdown();
                    }
                }
                else if (GetFileExtension == FileExtension.CrosswordListFile)
                {
                    MainWindow mainWindow = ((MainWindow)App.Current.MainWindow);
                    mainWindow.ContentSource = new Uri("/Pages/ListEditor.xaml", UriKind.Relative);
                }
                else
                {
                    Handled();
                    Messages.OpenFileError("Неверный формат файла.");
                    App.Current.Shutdown();
                }
            }
        }

        /// <summary>
        /// Устанавливает значение, что файл был обработан.
        /// </summary>
        public static void Handled()
        { wasHandled = true; }

        /// <summary>
        /// Возвращает путь к файлу, полученному при запуске приложения.
        /// </summary>
        public static string GetFilePath
        {
            get
            {
                if (wasHandled == false)
                { return filePath; }
                else
                { return String.Empty; }
            }
        }

        /// <summary>
        /// Возвращает расширение файла, полученного при запуске приложения.
        /// </summary>
        public static FileExtension GetFileExtension
        {
            get
            {
                if (fileExtension == ".cwtf")
                { return FileExtension.CrosswordListFile; }
                else if (fileExtension == ".cwgf")
                { return FileExtension.CrosswordGridFile; }
                else
                { return FileExtension.NotSupportedFileExtension; }
            }
        }
    }

    /// <summary>
    /// Перечисление расширений файлов Crossword Creator.
    /// </summary>
    public enum FileExtension
    {
        /// <summary>
        /// Файл списка терминов.
        /// </summary>
        CrosswordListFile,
        /// <summary>
        /// Файл сетки кроссворда.
        /// </summary>
        CrosswordGridFile,
        /// <summary>
        /// Неподдерживаемый тип файла.
        /// </summary>
        NotSupportedFileExtension
    }
}
