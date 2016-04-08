using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Crossword_Application_Modern.Startup
{
    /// <summary>
    /// Логика взаимодействия для CustomSplashScreen.xaml
    /// </summary>
    public partial class CustomSplashScreen : Window
    {
        public CustomSplashScreen()
        {
            InitializeComponent();

            // Установка позиции окна в центре доступного пространства.
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;

            // Заполнение поля версии значением.
            versionTB.Text = String.Format("Версия: {0}", Updater.GetCurrentVersion.ToString());
        }
    }
}
