using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Crossword_Application_Modern.Startup;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {       
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Settings.Load();
            CustomSplashScreen splashScreen = new CustomSplashScreen();
            splashScreen.Show();


            var startupTask = new Task(() =>
            {
                // Подгрузка чего-нибудь не в UI-потоке.
                Thread.Sleep(1200);
            });

            // В завершение показываем окно.
            startupTask.ContinueWith(t =>
            {
                if (Crossword_Application_Modern.Properties.Settings.Default.IsFirstStart == true)
                {
                    MainWindow mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    InitConfigurationWindow initWindow = new InitConfigurationWindow();
                    
                    initWindow.Loaded += (sender, args) => splashScreen.Close();
                    initWindow.ShowDialog();
                    Crossword_Application_Modern.Properties.Settings.Default.IsFirstStart = false;
                     
                    mainWindow.Show();
                }
                else
                { 
                    MainWindow mainWindow = new MainWindow();

                    //when main windows is loaded close splash screen
                    mainWindow.Loaded += (sender, args) => splashScreen.Close();

                    //set application main window;
                    this.MainWindow = mainWindow;

                    //and finally show it
                    mainWindow.Show();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupTask.Start();                       
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Save();
            base.OnExit(e);
        }        
    }
}
