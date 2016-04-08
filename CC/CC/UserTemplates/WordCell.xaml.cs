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
using Crossword_Application_Modern.ViewModel;

namespace Crossword_Application_Modern.UserTemplates
{
    /// <summary>
    /// Логика взаимодействия для WordCell.xaml
    /// </summary>
    public partial class WordCell : UserControl, INotifyPropertyChanged
    {
        public WordCell()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private char _symbol;

        /// <summary>
        /// Возвращает или задает значение символа WordCell.
        /// </summary>
        public char Symbol
        {
            get { return _symbol; }
            set
            {
                char _item = char.ToLower(value);
                if (_item != _symbol)
                {
                    _symbol = _item;
                    OnPropertyChanged("Symbol");
                }
            }
        }

        private int _id;

        /// <summary>
        /// Возвращает или задает значение числа, отображаемого в WordCell.
        /// </summary>
        public int ID
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        private bool _idIsVisible;

        /// <summary>
        /// Возвращает или задает значение, отображается ли число в WordCell.
        /// </summary>
        public bool IDIsVisible
        {
            get { return _idIsVisible; }
            set
            {
                if (value != _idIsVisible)
                {
                    _idIsVisible = value;
                    OnPropertyChanged("IDIsVisible");
                }
            }
        }
        
        /// <summary>
        /// Возникает, когда значение свойства изменено.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Уведомляет клиентов об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
