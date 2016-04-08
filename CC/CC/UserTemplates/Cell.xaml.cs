using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;

namespace Crossword_Application_Modern.UserTemplates
{
    /// <summary>
    /// Логика взаимодействия для cell.xaml
    /// </summary>
    public partial class Cell : UserControl, INotifyPropertyChanged
    {
        private Brush defaultBrush;
        private bool intersected = false;
        public Cell(char symbol)
        {
            InitializeComponent();
            this.DataContext = this;
            symbolTextBlock.Text = symbol.ToString();
            defaultBrush = rect.Fill;
        }

        private string _symbol = String.Empty;
        /// <summary>
        /// Возвращает или задает значение символа в клетке.
        /// </summary>
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (value != _symbol)
                {
                    _symbol = value;
                    OnPropertyChanged("Symbol");
                }
            }
        }

        public Cell()
        {
            InitializeComponent();
            symbolTextBlock.Text = " ";            
        }

        public void SetBackgroundColor(Color _color)
        {
            Brush _brush = new SolidColorBrush(_color);
            if (intersected == true)
            {
                rect.Fill = _brush;
            }             
        }

        public void SetDefaultBackroundBrush()
        {
            if (intersected == false)
            {
                rect.Fill = defaultBrush;
            }            
        }

        public bool IsIntersected
        {
            get { return intersected; }
            set { intersected = value; }
        }

        public void SetBorderBrush(Brush brush)
        { rect.Stroke = brush; }

        public void SetSymbol(char symbol)
        {
            symbolTextBlock.Text = symbol.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
