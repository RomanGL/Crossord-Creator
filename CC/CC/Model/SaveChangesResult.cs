using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossword_Application_Modern.Model
{
    /// <summary>
    /// Перечисление результатов попытки сохранения изменений.
    /// </summary>
    public enum SaveChangesResult
    {
        /// <summary>
        /// Сохранено.
        /// </summary>
        Saved, 
        /// <summary>
        /// Не сохранено.
        /// </summary>
        DontSaved, 
        /// <summary>
        /// Сохранение не требуется.
        /// </summary>
        NotRequired, 
        /// <summary>
        /// Операция отменена.
        /// </summary>
        Cancel
    }
}
