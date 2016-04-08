using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern.Model
{
    /// <summary>
    /// Базовая абстрактная модель слова.
    /// </summary>
    public abstract class BaseWordModel
    {
        /// <summary>
        /// Возвращает или задает значени ответа.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Возвращает или задает значение вопроса.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Возвращает или задает значение идентификатора.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Возвращает значение длины <see cref="Answer"/>.
        /// </summary>
        public int Length
        { get { return Answer.Length; } }
    }
}
