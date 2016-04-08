using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern.ViewModel
{
    /// <summary>
    /// Модель представления WordCell.
    /// </summary>
    public class WordCellViewModel : BaseViewModel
    {
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
    }
}
