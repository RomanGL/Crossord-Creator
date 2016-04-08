using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Класс слова сетки.
    /// (Устарел. Будет удален с завершением работы над новым редактором сетки и функцией заполнения)
    /// </summary>
    public class GridWord : BaseWord
    {
        private double posX;
        private double posY;
        private int gridID;
        private Orientation orientation;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GridWord"/> с заданными значениями.
        /// </summary>
        /// <param name="_word">Слово.</param>
        /// <param name="_question">Вопрос.</param>
        /// <param name="_ID">Идентификатор.</param>
        /// <param name="_x">Позиция по оси X относительно верхнего левого угла.</param>
        /// <param name="_y">Позиция по оси Y относительно верхнего левого угла.</param>
        /// <param name="_orientation">Ориентация слова.</param>
        public GridWord(string _word, string _question, int _ID, 
            double _x, double _y, Orientation _orientation)
        {
            Clear();

            Word = _word;
            Question = _question;
            gridID = _ID;
            ID = _ID;
            posX = _x;
            posY = _y;
            orientation = _orientation;
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GridWord"/>.
        /// </summary>
        public GridWord()
        { Clear(); }

        /// <summary>
        /// Восстанавливает значения всех свойств класса на параметры по умолчанию.
        /// </summary>
        public void Clear()
        {
            posX = 0;
            posY = 0;
            gridID = 0;
            ID = 0;
            Word = String.Empty;
            Question = String.Empty;
            orientation = Orientation.Vertical;
        }

        /// <summary>
        /// Возвращает или задает позицию слова по оси X относительно верхнего левого угла.
        /// </summary>
        public double X
        {
            get { return posX; }
            set { posX = value; }
        }

        /// <summary>
        /// Возвращает или задает позицию слова по оси Y относительно верхнего левого угла.
        /// </summary>
        public double Y
        {
            get { return posY; }
            set { posY = value; }
        }

        /// <summary>
        /// Возвращает или задает идентификатор слова.
        /// </summary>
        public int GridID
        {
            get { return gridID; }
            set { gridID = value; }
        }

        /// <summary>
        /// Возвращает или задает ориентацию слова.
        /// </summary>
        public Orientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }
    }
}
