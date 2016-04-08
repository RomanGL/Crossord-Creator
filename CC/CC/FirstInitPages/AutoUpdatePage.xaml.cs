using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Media.Animation;

namespace Crossword_Application_Modern.FirstInitPages
{
    /// <summary>
    /// Interaction logic for AutoUpdatePage.xaml
    /// </summary>
    public partial class AutoUpdatePage : UserControl
    {
        public AutoUpdatePage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitConfigurationWindow initWindow = ((InitConfigurationWindow)App.Current.Windows[1]);
            initWindow.ContentSource = new Uri("/FirstInitPages/FinalPage.xaml", UriKind.Relative);
        }
    }
}
