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
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CustomSplashScreen splashScreen = null;
            var startupTask = new Task(() =>
            {
                Settings.Load();
                try
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        splashScreen = new CustomSplashScreen();
                        splashScreen.Show();
                    }));
                }
                catch (Exception)
                {
                }               
            });

            // В завершение показываем окно.
            startupTask.ContinueWith(t =>
            {
                if (Crossword_Application_Modern.Properties.Settings.Default.IsFirstStart == true)
                {
                    MainWindow mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    InitConfigurationWindow initWindow = new InitConfigurationWindow();

                    initWindow.Loaded += (s, args) =>
                    {
                        if (splashScreen != null)
                            splashScreen.Close();
                    };
                    initWindow.ShowDialog();
                    Crossword_Application_Modern.Properties.Settings.Default.IsFirstStart = false;
                     
                    mainWindow.Show();
                }
                else
                { 
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Loaded += (s, args) =>
                    {
                        if (splashScreen != null)
                            splashScreen.Close();
                    };

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
