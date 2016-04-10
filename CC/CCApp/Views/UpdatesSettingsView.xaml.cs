﻿using FirstFloor.ModernUI.Windows.Controls;
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

namespace CCApp.Views
{
    /// <summary>
    /// Interaction logic for UpdatesSettingsView.xaml
    /// </summary>
    public partial class UpdatesSettingsView : UserControl
    {
        public UpdatesSettingsView()
        {
            InitializeComponent();
            this.Loaded += (s, e) => ModernDialog.ShowMessage("Тестовое сообщение", "Заголовок окна", MessageBoxButton.OK);
        }
    }
}
