using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public static bool playInitAnimation { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            playInitAnimation = true;                        
        }

        /// <summary>
        /// Обработчик события, происходящего, когда окно загружено и готово к визуализации.
        /// </summary>
        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            InitialFilePath.Prepare();
            BlurHandler.StartBlur += Messages_StartBlur;
            BlurHandler.StartUnBlur += Messages_StartUnBlur;

            this.WindowState = Common.MainWindowSettings.State;
            this.Height = Common.MainWindowSettings.Height;
            this.Width = Common.MainWindowSettings.Width;

            if (Settings.ThemeRestored == true)
            { Messages.UserBackgroundFileNotFound(); }
            Updater.CheckUpdates(false);
        }

        /// <summary>
        /// Обработчик события, происходящего перед закрытием окна.
        /// </summary>
        private void modernWindow_Closing(object sender, CancelEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    Common.MainWindowSettings.State = System.Windows.WindowState.Maximized;
                    break;
                case WindowState.Normal:
                    Common.MainWindowSettings.State = System.Windows.WindowState.Normal;
                    Common.MainWindowSettings.Height = this.Height;
                    Common.MainWindowSettings.Width = this.Width;
                    break;
            }            
        }

        private void Messages_StartUnBlur()
        { StartUnBlurAnimation(); }

        private void Messages_StartBlur()
        { StartBlurAnimation(); }


        #region Animations
        private void StartBlurAnimation()
        {
            Storyboard BlurAnimation = (Storyboard)this.Resources["BlurAnimation"];
            BlurAnimation.Begin();
        }

        private void StartUnBlurAnimation()
        {
            Storyboard UnBlurAnimation = (Storyboard)this.Resources["UnBlurAnimation"];
            UnBlurAnimation.Begin();
        }
        #endregion         
    }
}
