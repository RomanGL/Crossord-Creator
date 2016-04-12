namespace CC.Core.Models
{
    /// <summary>
    /// Тип формата файла Crossword Creator.
    /// </summary>
    public enum CCFileFormat : byte
    {
        /// <summary>
        /// Неверный формат файла.
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Xml-формат файла.
        /// </summary>
        Xml,
        /// <summary>
        /// Json-формат файла.
        /// </summary>
        Json
    }
}
