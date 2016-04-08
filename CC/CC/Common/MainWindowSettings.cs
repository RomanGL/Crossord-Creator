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

namespace Crossword_Application_Modern.Common
{
    /// <summary>
    /// Статический класс, предназначенный для хранения настроек главного окна.
    /// </summary>
    public static class MainWindowSettings
    {
        private static WindowState _state = new WindowState();
        private static double _height;
        private static double _width;

        /// <summary>
        /// Возвращает или задает значение состояния окна.
        /// </summary>
        public static WindowState State
        {
            get { return _state; }
            set
            {
                if (value != _state)
                { _state = value; }
            }
        }

        /// <summary>
        /// Возвращает или задает значение высоты окна.
        /// </summary>
        public static double Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                { _height = value; }
            }
        }

        /// <summary>
        /// Возвращает или задает значение ширины окна.
        /// </summary>
        public static double Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                { _width = value; }
            }
        }
    }
}
