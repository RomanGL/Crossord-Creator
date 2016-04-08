using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, предназначенный для работы с настройками приложения.
    /// </summary>
    static class Settings
    {
        private static bool themeWasRestored = false;

        /// <summary>
        /// Возвращает значение, восстановлена ли тема на стандартную.
        /// </summary>
        public static bool ThemeRestored
        { get { return themeWasRestored; } }

        /// <summary>
        /// Сохраняет настройки приложения.
        /// </summary>
        public static void Save()
        {
            Crossword_Application_Modern.Properties.Settings.Default.FontSize =
                AppearanceManager.Current.FontSize;
            Crossword_Application_Modern.Properties.Settings.Default.AccentColorR =
                AppearanceManager.Current.AccentColor.R;
            Crossword_Application_Modern.Properties.Settings.Default.AccentColorG =
                AppearanceManager.Current.AccentColor.G;
            Crossword_Application_Modern.Properties.Settings.Default.AccentColorB =
                AppearanceManager.Current.AccentColor.B;
            Crossword_Application_Modern.Properties.Settings.Default.ThemeSource = 
                AppearanceManager.Current.ThemeSource.ToString();
            Crossword_Application_Modern.Properties.Settings.Default.AutoUpdatesEnabled =
                Updater.AutoUpdatesEnabled;

            Crossword_Application_Modern.Properties.Settings.Default.WindowState =
                Common.MainWindowSettings.State;
            Crossword_Application_Modern.Properties.Settings.Default.WindowHeight =
                Common.MainWindowSettings.Height;
            Crossword_Application_Modern.Properties.Settings.Default.WindowWidth =
                Common.MainWindowSettings.Width;

            Crossword_Application_Modern.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Применяет настройки приложения.
        /// </summary>
        public static void Load()
        {
            if (Crossword_Application_Modern.Properties.Settings.Default.CallUpgrade == true)
            {
                Crossword_Application_Modern.Properties.Settings.Default.Upgrade();
                Crossword_Application_Modern.Properties.Settings.Default.CallUpgrade = false;
            }            
           
            Uri _themeSource = new Uri(Crossword_Application_Modern.Properties.Settings.Default.ThemeSource, UriKind.Relative);
            
            if (_themeSource == new Uri("/Themes/UserLightTheme.xaml", UriKind.Relative))
            {
                if(Themes.UserBackgroundFileExist.CheckBackgroundFileExist())
                { AppearanceManager.Current.ThemeSource = _themeSource; }
                else
                { 
                    AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource;
                    themeWasRestored = true;
                }
            }
            else if (_themeSource == new Uri("/Themes/UserDarkTheme.xaml", UriKind.Relative))
            {
                if (Themes.UserBackgroundFileExist.CheckBackgroundFileExist())
                { AppearanceManager.Current.ThemeSource = _themeSource; }
                else
                { 
                    AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
                    themeWasRestored = true;
                }
            }
            else
            { AppearanceManager.Current.ThemeSource = _themeSource; }

            byte r, g, b;
            r = Crossword_Application_Modern.Properties.Settings.Default.AccentColorR;
            g = Crossword_Application_Modern.Properties.Settings.Default.AccentColorG;
            b = Crossword_Application_Modern.Properties.Settings.Default.AccentColorB;

            AppearanceManager.Current.AccentColor = Color.FromRgb(r, g, b);
            AppearanceManager.Current.FontSize =
                Crossword_Application_Modern.Properties.Settings.Default.FontSize;

            Updater.AutoUpdatesEnabled = 
                Crossword_Application_Modern.Properties.Settings.Default.AutoUpdatesEnabled;

            Common.MainWindowSettings.State = 
                Crossword_Application_Modern.Properties.Settings.Default.WindowState;
            Common.MainWindowSettings.Height =
                Crossword_Application_Modern.Properties.Settings.Default.WindowHeight;
            Common.MainWindowSettings.Width =
                Crossword_Application_Modern.Properties.Settings.Default.WindowWidth;
        }
    }
}
