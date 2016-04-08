using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
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
        private const string FrameworkNameMask = "Microsoft .Net Framework {0} или новее";
        private static int frameworkVersion = 0;
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
        public static event Updates UpdateIsAvailable;

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
            frameworkVersion = GetFrameworkVersion();
            if (frameworkVersion == 0)
                UpdateOldVersion();
            else
                UpdateNewVersion(frameworkVersion);
        }

        private static void UpdateNewVersion(int frameworkVersion)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(UpdateInfo2.UpdateInfoUrl));
        }

        private static void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (visible)
                    Messages.CheckUpdatesFailure(e.Error.Message);

                if (CheckUpdatesFail != null)
                    CheckUpdatesFail();

                return;
            }
                        
            try
            {
                var currentVersion = GetCurrentVersion;
                var updateInfo = JsonConvert.DeserializeObject<UpdateInfo2>(e.Result);
                newVersion = Version.Parse(updateInfo.NewVersion);                
                whatsNew = updateInfo.Changes;
                updatePath = updateInfo.Source;

                if (newVersion > currentVersion)
                {
                    if (frameworkVersion < updateInfo.FrameworkVersion)
                    {
                        Messages.ShowMessage(String.Format("Стала доступна новая версия Crossword Creator, однако ее нельзя установить.\nДля установки требуется:\n{0}.",
                            GetFrameworkVersionName(updateInfo.FrameworkVersion)), 
                            "Невозможно установить обновление");
                    }
                    else
                    {
                        if (UpdateIsAvailable != null)
                            UpdateIsAvailable();

                        if (Messages.NewVersionAvaible() == System.Windows.MessageBoxResult.Yes)
                        {
                            UpdateWindow updateWindow = new UpdateWindow();
                            updateWindow.Show();
                        }

                        if (UpdateMessageClosed != null)
                            UpdateMessageClosed();
                    }
                }
                else if (visible)
                {
                    Messages.NoUpdatesAvailable();
                }
            }
            catch (Exception ex)
            {
                if (visible)
                    Messages.CheckUpdatesFailure(ex.Message);

                if (CheckUpdatesFail != null)
                    CheckUpdatesFail();

                return;
            }

            if (CheckUpdatesFinished != null)
                CheckUpdatesFinished();
        }

        private static void UpdateOldVersion()
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += oldClient_OpenReadCompleted;
            client.OpenReadAsync(new Uri("http://crosswordcreator.esy.es/app/update.txt"));
        }

        /// <summary>
        /// Обработчик события завершения загрузки файла обновления.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void oldClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
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
                        if (UpdateIsAvailable != null)
                        { UpdateIsAvailable(); }
                        if (Messages.NewVersionAvaible() == System.Windows.MessageBoxResult.Yes)
                        {
                            UpdateWindow updateWindow = new UpdateWindow();
                            updateWindow.Show();
                        }
                        if (UpdateMessageClosed != null)
                        { UpdateMessageClosed(); }
                    }
                    else
                    {
                        if (visible == true)
                        { Messages.NoUpdatesAvailable(); }
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

        private static int GetFrameworkVersion()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                .OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                var version = ndpKey.GetValue("Release");
                if (version == null)
                {
                    return 0; // Windows XP, Net 4.0.
                }
                else
                {
                    return Convert.ToInt32(version);
                }
            }
        }

        private static string GetFrameworkVersionName(int version)
        {
            if (version >= 393295)
            {
                return String.Format(FrameworkNameMask, "4.6");
            }
            if (version >= 379893)
            {
                return String.Format(FrameworkNameMask, "4.5.2");
            }
            if (version >= 378675)
            {
                return String.Format(FrameworkNameMask, "4.5.1");
            }
            if (version >= 378389)
            {
                return String.Format(FrameworkNameMask, "4.5");
            }
            return String.Format(FrameworkNameMask, "4 Full");
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
