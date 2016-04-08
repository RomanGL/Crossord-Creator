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

namespace Crossword_Application_Modern.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            if (MainWindow.playInitAnimation == true)
            {
                StartFirstAnimation();
                MainWindow.playInitAnimation = false;
            }
        }

        private void StartFirstAnimation()
        {
            Storyboard BlurAnimation = (Storyboard)this.Resources["InitAnimation"];
            BlurAnimation.Begin();
        }
    }
}
