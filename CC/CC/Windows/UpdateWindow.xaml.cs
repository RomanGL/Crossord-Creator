using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public partial class UpdateWindow : ModernWindow
    {
        public UpdateWindow()
        {
            InitializeComponent();                   
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BlurHandler.StartBlur += BlurHandler_StartBlur;
            BlurHandler.StartUnBlur += BlurHandler_StartUnBlur;
            Crossword_Application_Modern.Content.UpdatesPage.DownloadUpdateStarted += 
                UpdatesPage_DownloadUpdateStarted;
            Crossword_Application_Modern.Content.UpdatesPage.DownloadUpdateComplete += 
                UpdatesPage_DownloadUpdateComplete;
        }

        void UpdatesPage_DownloadUpdateComplete()
        {
            this.IsEnabled = true;
        }

        void UpdatesPage_DownloadUpdateStarted()
        {
            this.IsEnabled = false;
        }

        private void BlurHandler_StartUnBlur()
        { StartUnBlurAnimation(); }

        private void BlurHandler_StartBlur()
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
