using CC.Core.Models.IO;
using System;

namespace CC.Core.Services
{
    /// <summary>
    /// Это исключени выбрасывает, когда не удалось обработать файл Crossword Creator.
    /// </summary>
    public class CCFileException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CCFileException"/>.
        /// </summary>
        /// <param name="file">Файл, ставший причиной исключения.</param>
        public CCFileException(IFile file) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CCFileException"/> с указанным файлом
        /// и описанием ошибки.
        /// </summary>
        /// <param name="file">Файл, ставший причиной исключения.</param>
        /// <param name="message">Описание ошибки.</param>
        public CCFileException(IFile file, string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CCFileException"/>с указанным файлом,
        /// описанием ошибки и исключением, ставшим причиной данного исключения.
        /// </summary>
        /// <param name="file">Файл, ставший причиной исключения.</param>
        /// <param name="message">Описание ошибки.</param>
        /// <param name="inner">Исключение, ставшее причиной данного исключения.</param>
        public CCFileException(IFile file, string message, Exception inner) : base(message, inner) { }
    }
}
