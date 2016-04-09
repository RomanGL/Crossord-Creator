using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Crossword_Application_Modern.Content
{
    /// <summary>
    /// Interaction logic for UpdatesPage.xaml
    /// </summary>
    public partial class UpdatesPage : UserControl
    {
        public delegate void DownloadStarted();
        public delegate void DownloadComplete();
        public static event DownloadStarted DownloadUpdateStarted;
        public static event DownloadComplete DownloadUpdateComplete;

        private List<string> whatsNewList = Updater.GetWhatsNew;
        private string updatePath = String.Format("{0}/updateSetup.exe", 
            AppLocalDirectory.GetApplicationLocalDirectory);
        private WebClient client = new WebClient();

        public UpdatesPage()
        {
            InitializeComponent();
            newVersionTextBlock.Text = String.Format("Новая версия {0}",
                Updater.GetNewVersion.ToString());
            currentVersionTextBlock.Text = String.Format("Текущая версия {0}",
                Updater.GetCurrentVersion.ToString());
            whatsNewListBox.ItemsSource = whatsNewList;

            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppLocalDirectory.CreateApplicationDirectory();
                client.DownloadFileAsync(new Uri(Updater.GetUpdatePath), updatePath.Replace('/', '\\'));
                percentageTextBlock.Visibility = System.Windows.Visibility.Visible;
                downloadButton.IsEnabled = false;

                if (DownloadUpdateStarted != null)
                { DownloadUpdateStarted(); }
            }
            catch (Exception ex)
            {
                Messages.DownloadUpdateCompleteFailure(ex.Message);
            }
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            downloadButton.IsEnabled = true;

            if (DownloadUpdateComplete != null)
            { DownloadUpdateComplete(); }

            if (e.Error == null)
            {
                Messages.DownloadUpdateCompleteSuccessfully();
                try
                {
                    Process.Start(updatePath);
                    App.Current.Shutdown();
                }
                catch (Exception)
                {
                    Messages.ShowMessage("Не удалось запустить программу установки. Повторите попытку позже.",
                        "Не удалось выполнить обновление");
                }
            }
            else
            {
                percentageTextBlock.Visibility = System.Windows.Visibility.Hidden;
                downloadProgressBar.Value = 0;
                Messages.DownloadUpdateCompleteFailure(e.Error.Message); 
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            percentageTextBlock.Text = String.Format("{0}%", e.ProgressPercentage);
            downloadProgressBar.Value = e.ProgressPercentage;
        }
    }
}
