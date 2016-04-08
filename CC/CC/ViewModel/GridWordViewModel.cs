using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crossword_Application_Modern.Model;
using System.Windows.Controls;

namespace Crossword_Application_Modern.ViewModel
{
    /// <summary>
    /// Модель представления слова на сетке.
    /// </summary>
    public class GridWordViewModel : BaseViewModel
    {
        private GridWordModel _model;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GridWordViewModel"/> с заданным
        /// экземпляром <seealso cref="GridWordModel"/>.
        /// </summary>
        /// <param name="model">Модель слова сетки.</param>
        public GridWordViewModel(GridWordModel model)
        { _model = model; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GridWordViewModel"/>.
        /// </summary>
        public GridWordViewModel()
        { _model = new GridWordModel(); }

        /// <summary>
        /// Возвращает текущую модель данных <seealso cref="GridWordModel"/>.
        /// </summary>
        public GridWordModel GetModel
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
        /// Возвращает или задает позицию слова по оси X относительно верхнего левого угла.
        /// </summary>
        public double X
        {
            get { return _model.X; }
            set
            {
                if (value != _model.X)
                {
                    _model.X = value;
                    OnPropertyChanged("X");
                }
            }
        }

        /// <summary>
        /// Возвращает или задает позицию слова по оси Y относительно верхнего левого угла.
        /// </summary>
        public double Y
        {
            get { return _model.Y; }
            set
            {
                if (value != _model.Y)
                {
                    _model.Y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        /// <summary>
        /// Возвращает или задает ориентацию слова.
        /// </summary>
        public Orientation Orientation
        {
            get { return _model.Orientation; }
            set
            {
                if (value != _model.Orientation)
                {
                    _model.Orientation = value;
                    OnPropertyChanged("Orientation");
                }
            }
        }
    }
}
