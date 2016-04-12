namespace CC.Core.Models
{
    /// <summary>
    /// Представляет версию приложения Crossword Creator.
    /// </summary>
    public sealed class CCVersion
    {
        /// <summary>
        /// Стандартное название приложения.
        /// </summary>
        public const string DEFAULT_APPLICATION_NAME = "Crossword Creator Desktop";

        /// <summary>
        /// Возвращает или задает имя приложения.
        /// </summary>
        public string ApplicationName { get; set; } = DEFAULT_APPLICATION_NAME;

        /// <summary>
        /// Мажорная версия приложения.
        /// </summary>
        public uint Major { get; set; } = 1;

        /// <summary>
        /// Минорная версия приложения.
        /// </summary>
        public uint Minor { get; set; }

        /// <summary>
        /// Версия сборки приложения.
        /// </summary>
        public uint Build { get; set; }

        /// <summary>
        /// Ревизия приложения.
        /// </summary>
        public uint Revision { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CCVersion"/> со значениями по умолчанию.
        /// </summary>
        public CCVersion()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CCVersion"/> с указанным именем приложения.
        /// </summary>
        /// <param name="appName">Имя приложения.</param>
        public CCVersion(string appName)
        {
            ApplicationName = appName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CCVersion"/> с указанным именем и версией приложения.
        /// </summary>
        /// <param name="appName">Имя приложения.</param>
        /// <param name="major">Мажорная версия приложения.</param>
        /// <param name="minor">Минорная версия приложения</param>
        /// <param name="build">Версия сборки приложения.</param>
        /// <param name="revision">Версия ревизии приложения.</param>
        public CCVersion(string appName, uint major, uint minor, uint build, uint revision)
        {
            ApplicationName = appName;
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public override string ToString()
        {
            return $"{ApplicationName} {Major}.{Minor}.{Build}.{Revision}";
        }
    }
}
