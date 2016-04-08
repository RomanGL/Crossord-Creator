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
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class SettingsAppearanceFirstInitPage : UserControl
    {
        public SettingsAppearanceFirstInitPage()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new SettingsAppearanceViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitConfigurationWindow initWindow = ((InitConfigurationWindow)App.Current.Windows[1]);
            initWindow.ContentSource = new Uri("/FirstInitPages/AutoUpdatePage.xaml", UriKind.Relative);
        }
    }
}
