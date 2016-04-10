using CCApp.Common;
using System.Windows;

namespace CCApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _botstrapper = new CCBootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _botstrapper.Run();
            base.OnStartup(e);
        }

        private readonly CCBootstrapper _botstrapper;
    }
}
