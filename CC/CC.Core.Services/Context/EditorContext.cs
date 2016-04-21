namespace CC.Core.Services.Context
{
    /// <summary>
    /// Представет контекст редактора Crossword Creator.
    /// </summary>
    public abstract class EditorContext
    {
        /// <summary>
        /// Имеются несохраненные изменения.
        /// </summary>
        public bool IsDirty { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public abstract bool Save(bool asNew);
        public abstract void Open();
        public abstract void Open(string filePath);
        public abstract void New();
    }
}
