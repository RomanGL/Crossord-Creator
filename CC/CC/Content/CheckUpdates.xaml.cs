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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crossword_Application_Modern.Content
{
    /// <summary>
    /// Interaction logic for CheckUpdatesxaml.xaml
    /// </summary>
    public partial class CheckUpdates : UserControl
    {
        public CheckUpdates()
        {
            InitializeComponent();
            CheckingWorking.Visibility = System.Windows.Visibility.Collapsed;
            autoUpdateIsEnabled.IsChecked = Updater.AutoUpdatesEnabled;
        }

        private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            Updater.CheckUpdatesFinished += Updater_CheckUpdatesFinished;
            CheckingWorking.Visibility = System.Windows.Visibility.Visible;
            Updater.CheckUpdates(true);
        }

        void Updater_CheckUpdatesFinished()
        {
            CheckingWorking.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void autoUpdateIsEnabled_Checked(object sender, RoutedEventArgs e)
        {
            Updater.AutoUpdatesEnabled = true;
        }

        private void autoUpdateIsEnabled_Unchecked(object sender, RoutedEventArgs e)
        {
            Updater.AutoUpdatesEnabled = false;
        }
    }
}
