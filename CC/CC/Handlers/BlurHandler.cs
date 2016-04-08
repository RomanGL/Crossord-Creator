using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, предназначенный для вызова анимации размытия открытых окон приложения.
    /// </summary>
    public static class BlurHandler
    {
        public delegate void MessageDelegate();

        /// <summary>
        /// Происходит, когда требуется включить размытие.
        /// </summary>
        public static event MessageDelegate StartBlur;

        /// <summary>
        /// Происходит, когда требуется отключить размытие.
        /// </summary>
        public static event MessageDelegate StartUnBlur;

        /// <summary>
        /// Вызывает размытие активных окон приложения.
        /// </summary>
        /// <param name="operation">true - размыть, false - убрать размытость</param>
        public static void Blur(bool operation)
        {
            if (operation == true)
            {
                if (StartBlur != null)
                { StartBlur(); }
            }
            else
            {
                if (StartUnBlur != null)
                { StartUnBlur(); }
            }
        }
    }
}
