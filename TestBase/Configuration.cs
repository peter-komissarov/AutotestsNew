namespace TestBase
{
    /// <summary>
    /// Константы для окружений
    /// </summary>
    public static class Configuration
    {
#if PRODUCTION
        #region Production environment constants

        // Базовые Uri
        public const string GitHubBase = "production_environment_base_url";

        // Строки подключения в базам данных
        public const string Epayments = "production_environment_connections_string";
                
        // Другое
        public const string DateTimeFormat = "production_environment_datetime_format";
        public const string Language = "test_environment_language";

        #endregion

#elif SANDBOX
        #region Sandbox environment constants

        // Базовые Uri
        public const string GitHubBase = "sandbox_environment_base_url";

        // Строки подключения в базам данных
        public const string Epayments = "sandbox_environment_connections_string";
        
        // Другое
        public const string DateTimeFormat = "sandbox_environment_datetime_format";
        public const string Language = "sandbox_environment_language";

        #endregion

#else

        #region Test environment constants

        // Базовые Uri
        public const string GitHubBase = "https://api.github.com/";

        // Строки подключения в базам данных
        public const string Epayments = "data source=SCV-BD01.swiftcom.corp;"
            + "initial catalog=test_epayments;"
            + "Integrated Security=false;"
            + "User ID=dev;"
            + "Password=uchMeIvank;"
            + "Max Pool Size=1024;"
            + "Pooling=true;"
            + "Connection Timeout=15";

        // Другое
        public const string DateTimeFormat = "dddd, dd MMMM yyyy HH:mm:ss.fff";
        public const string Language = "ru-RU";

        #endregion

#endif
    }
}