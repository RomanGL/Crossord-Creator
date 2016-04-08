using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Базовый абстрактный класс слова.
    /// (Устарел. Будет удален с завершением работы над новым редактором сетки и функцией заполнения)
    /// </summary>
    public abstract class BaseWord : INotifyPropertyChanged
    {
        private string _word;               
        private string _question;
        public int ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public override string ToString()
        { return _word; }

        /// <summary>
        /// Возвращает или задает слово.
        /// </summary>
        public string Word
        {
            get { return _word; }
            set
            {
                _word = value;
                NotifyPropertyChanged("word");
            }
        }

        /// <summary>
        /// Вовзращает или задает вопрос.
        /// </summary>
        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                NotifyPropertyChanged("question");
            }
        }

        /// <summary>
        /// Возвращает длину слова.
        /// </summary>
        /// <returns></returns>
        public int Length()
        { return _word.Length; }

        /// <summary>
        /// Возвращает идентификатор слова.
        /// </summary>
        public int GetID
        {
            get { return ID; }
        }

        /// <summary>
        /// Уведомляет клиентов о том, что свойство изменилось.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected void NotifyPropertyChanged(string propertyName)
        { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
