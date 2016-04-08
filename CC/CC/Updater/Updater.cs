using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, предназначенный для проверки обновлений.
    /// </summary>
    static class Updater
    {
        public delegate void Updates();

        /// <summary>
        /// Ссылка на файл обновления.
        /// </summary>
        private static string updatePath = String.Empty;

        /// <summary>
        /// Указывает, требуется ли сообщать пользователю об ошибках во время проверки обновлений.
        /// </summary>
        private static bool visible = false;

        /// <summary>
        /// Текущая версия приложения.
        /// </summary>
        private static Version currentVersion = 
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Новая версия приложения.
        /// </summary>
        private static Version newVersion = Version.Parse("0.0.0.0");

        /// <summary>
        /// Список изменений в обновлении "Что нового".
        /// </summary>
        private static List<string> whatsNew = new List<string>();

        /// <summary>
        /// Происходит при наличии обновления.
        /// </summary>
        public static event Updates UpdateIsAvaible;

        /// <summary>
        /// Происходит, когда окна с результатом проверки обновлений закрыто.
        /// </summary>
        public static event Updates UpdateMessageClosed;

        /// <summary>
        /// Происходит, когда проверка обновлений завершилась неудачно (например, в следствие ошибки соединения).
        /// </summary>
        public static event Updates CheckUpdatesFail;

        /// <summary>
        /// Происходит, когда проверка обновлений завершена.
        /// </summary>
        public static event Updates CheckUpdatesFinished;

        /// <summary>
        /// Указывает, включена ли автоматическая проверка обновлений.
        /// </summary>
        private static bool autoUpdates = 
            Crossword_Application_Modern.Properties.Settings.Default.AutoUpdatesEnabled;

        /// <summary>
        /// Возвращает или задает значение, указывающее включена ли автоматическая проверка обновлений.
        /// </summary>
        public static bool AutoUpdatesEnabled
        {
            get { return autoUpdates; }
            set { autoUpdates = value; }
        }

        /// <summary>
        /// Запускает проверку наличия обновлений.
        /// </summary>
        /// <param name="_visible">Требуется ли отображать ошибки.</param>
        public static void CheckUpdates(bool _visible)
        {
            visible = _visible;
            if (visible == true)
            {
                Check();
            }
            else
            {
                if (AutoUpdatesEnabled == true)
                {
                    Check();
                }
            }
        }

        /// <summary>
        /// Запускает чтение файла обновления с сервера.
        /// </summary>
        private static void Check()
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += client_OpenReadCompleted;
            client.OpenReadAsync(new Uri("http://crosswordcreator.esy.es/app/update.txt"));
        }

        /// <summary>
        /// Обработчик события завершения загрузки файла обновления.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Stream stream = null;
            try
            {
                stream = e.Result;
            }
            catch (Exception ex)
            {                
                if (visible == true)
                {
                    Messages.CheckUpdatesFailure(ex.InnerException.Message);
                }
                if (CheckUpdatesFail != null)
                { CheckUpdatesFail(); }
            }

            if (stream != null)
            {
                StreamReader sr = new StreamReader(stream);

                if (Version.TryParse(sr.ReadLine(), out newVersion))
                {
                    whatsNew.Clear();
                    string line;
                    if ((line = sr.ReadLine()) != null)
                    { updatePath = line; }
                    else
                    { updatePath = String.Empty; }
                    while ((line = sr.ReadLine()) != null)
                    { whatsNew.Add(line); }

                    stream.Close();

                    
                    if ((currentVersion < newVersion) && !String.IsNullOrWhiteSpace(updatePath))
                    {
                        if (UpdateIsAvaible != null)
                        { UpdateIsAvaible(); }
                        if (Messages.NewVersionAvaible() == System.Windows.MessageBoxResult.Yes)
                        {
                            UpdateWindow updateWindow = new UpdateWindow();
                            updateWindow.Show();
                            //new ModernDialog
                            //{
                            //    Title = "Доступно обновление",
                            //    Content = new Content.UpdatesPage(),
                            //    MinWidth = 640,
                            //    MinHeight = 440
                            //}.Show();
                        }
                        if (UpdateMessageClosed != null)
                        { UpdateMessageClosed(); }
                    }
                    else
                    { 
                        if (visible == true)
                        { Messages.NoUpdatesAvaible(); }
                    }
                }
                else
                {                     
                    stream.Close();                                       
                }
            }

            if (CheckUpdatesFinished != null)
            { CheckUpdatesFinished(); }
        }

        /// <summary>
        /// Возвращает текущую версию приложения.
        /// </summary>
        public static Version GetCurrentVersion
        { get { return currentVersion; } }

        /// <summary>
        /// Вовзращает новую версию приложения (версия обновления).
        /// </summary>
        public static Version GetNewVersion
        { get { return newVersion; } }

        /// <summary>
        /// Возвращает список изменений в обновлении.
        /// </summary>
        public static List<string> GetWhatsNew
        { get { return whatsNew; } }

        /// <summary>
        /// Возвращает путь файла-установщика обновления.
        /// </summary>
        public static string GetUpdatePath
        { get { return updatePath; } }
    }
}
