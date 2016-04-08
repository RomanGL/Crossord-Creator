﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern.Model
{
    /// <summary>
    /// Класс слова списка терминов.
    /// </summary>
    public sealed class ListWordModel : BaseWordModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ListWordModel"/> с заданными значениями.
        /// </summary>
        /// <param name="answer">Ответ.</param>
        /// <param name="question">Вопрос.</param>
        /// <param name="id">Числовой идентификатор.</param>
        public ListWordModel(string answer, string question, int id)
        {
            Answer = answer;
            Question = question;
            ID = id;            
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ListWordModel"/>.
        /// </summary>
        public ListWordModel() { }
    }
}
