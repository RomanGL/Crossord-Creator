using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Класс слова старого редактора списков.
    /// (Устарел. Будет удален с завершением работы над новым редактором сетки и функцией заполнения)
    /// </summary>
    class EditorWord : BaseWord
    {
        /// <summary>
        /// Инииализирует новый экземпляр <see cref="EditorWord"/> с заданными значенийми.
        /// </summary>
        /// <param name="q">Вопрос.</param>
        /// <param name="a">Ответ (слово).</param>
        public EditorWord(string q, string a)
        {
            Question = q;
            Word = a;
        }

        /// <summary>
        /// Инииализирует новый экземпляр <see cref="EditorWord"/> с заданными значенийми.
        /// </summary>
        /// <param name="q">Вопрос.</param>
        /// <param name="a">Ответ (слово).</param>
        /// <param name="id">Идентификатор.</param>
        public EditorWord(string q, string a, int id)
        {
            Question = q;
            Word = a;
            ID = id;
        }
    }
}
