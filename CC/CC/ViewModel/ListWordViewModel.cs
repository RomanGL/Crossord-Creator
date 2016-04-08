using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crossword_Application_Modern.Model;

namespace Crossword_Application_Modern.ViewModel
{
    /// <summary>
    /// Модель представления слова списка терминов.
    /// </summary>
    public class ListWordViewModel : BaseViewModel
    {
        private ListWordModel _model;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ListWordViewModel"/> с заданным 
        /// экземпляром <seealso cref="ListWordModel"/>.
        /// </summary>
        /// <param name="model">Модель слова списка терминов.</param>
        public ListWordViewModel(ListWordModel model)
        { _model = model; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ListWordViewModel"/>.
        /// </summary>
        public ListWordViewModel()
        { _model = new ListWordModel(); }

        /// <summary>
        /// Возвращает текущую модель данных <seealso cref="ListWordModel"/>.
        /// </summary>
        public ListWordModel GetModel
        {
            get { return _model; }
        }

        /// <summary>
        /// Возвращает или задает значение ответа (в нижнем регистре).
        /// </summary>
        public string Answer
        {            
            get { return _model.Answer; }
            set
            {
                string _item = value.ToLower();
                if (_item != _model.Answer)
                {
                    _model.Answer = _item;
                    OnPropertyChanged("Answer");
                }
            }
        }

        /// <summary>
        /// Возвращает или задает значение вопроса.
        /// </summary>
        public string Question
        {
            get { return _model.Question; }
            set
            {
                if (value != _model.Question)
                {
                    _model.Question = value;
                    OnPropertyChanged("Question");
                }
            }
        }

        /// <summary>
        /// Возвращает или задает значение идентификатора слова.
        /// </summary>
        public int ID
        {
            get { return _model.ID; }
            set
            {
                if (value != _model.ID)
                {
                    _model.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        /// <summary>
        /// Возвращает значение длины <seealso cref="Answer"/>.
        /// </summary>
        public int Length
        { get { return _model.Length; } }
    }
}
