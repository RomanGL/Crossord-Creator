namespace CC.Core.Model
{
    /// <summary>
    /// Представляет файл Crossword Creator.
    /// Это абстрактный класс.
    /// </summary>
    /// <typeparam name="T">Тип содержимого файла.</typeparam>
    internal abstract class CCFile<T>
    {
        /// <summary>
        /// Версия приложения, создавшего файл.
        /// </summary>
        public CCVersion AppVersion { get; set; }

        /// <summary>
        /// Содержимое файла.
        /// </summary>
        public T Content { get; set; }
    }
}
