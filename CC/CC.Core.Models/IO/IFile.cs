namespace CC.Core.Models.IO
{
    /// <summary>
    /// Представляет файл.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Имя файла с расширением.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Абсолютный путь к файлу.
        /// </summary>
        string Path { get; set; }
    }
}
