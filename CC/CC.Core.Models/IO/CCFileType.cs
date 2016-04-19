namespace CC.Core.Models
{
    /// <summary>
    /// Формат файла Crossword Creator.
    /// </summary>
    public enum CCFileType
    {
        /// <summary>
        /// Неподдерживаемый формат.
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Файл списка терминов Crossword Creator 1.x.
        /// </summary>
        cwtf,
        /// <summary>
        /// Файл кроссворда Crossword Creator 1.x.
        /// </summary>
        cwgf,
        /// <summary>
        /// Файл списка терминов Crossword Creator 2.x.
        /// </summary>
        cwtx,
        /// <summary>
        /// Файл кроссворда Crossword Creator 2.x.
        /// </summary>
        cwgx
    }
}
