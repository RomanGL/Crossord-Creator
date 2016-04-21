using CC.Core.Services.Interfaces;
using CCApp.Common;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CCApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;  
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _botstrapper = new CCBootstrapper();            
            _botstrapper.Run();

            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            bool canStyled = true;
            if (_botstrapper != null)
            {
                try
                {
                    var msgService = _botstrapper.Container.TryResolve<IMessagesService>();
                    if (msgService != null)
                    {
                        msgService.ShowMessage(String.Format(ErrorTextMask, e.Exception.ToString()));
                    }
                }
                catch (Exception)
                {
                    canStyled = false;
                }
            }

            if (!canStyled)
            {
                MessageBox.Show(String.Format(ErrorTextMask, e.Exception.ToString()), ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            e.Handled = true;
            Shutdown();
        }
        
        private CCBootstrapper _botstrapper;

        private const string ErrorTextMask = "Произошла непредвиденная ошибка, приложение будет закрыто. Мы очень сожалеем о случившемся.\n\n{0}";
        private const string ErrorTitle = "Crossword Creator";
    }
}
