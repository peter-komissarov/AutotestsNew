namespace TestBase
{
    /// <summary>
    /// Constants per environment
    /// </summary>
    public static class Configuration
    {
#if PRODUCTION
        #region Production environment constants

        // Base URLs
        public const string GitHubBaseUrl = "production_environment_base_url";

        // Connections strings
        public const string TestDatabase = "production_environment_connections_string";
                
        // Others
        public const string DateTimeFormat = "production_environment_datetime_format";
        public const string Language = "test_environment_language";

        #endregion

#elif SANDBOX
        #region Sandbox environment constants

        // Base URLs
        public const string GitHubBaseUrl = "sandbox_environment_base_url";

        // Connections strings
        public const string TestDatabase = "sandbox_environment_connections_string";
        
        // Others
        public const string DateTimeFormat = "sandbox_environment_datetime_format";
        public const string Language = "sandbox_environment_language";

        #endregion

#else

        #region Test environment constants

        // Base URLs
        public const string GitHubBaseUrl = "https://api.github.com/";

        // Connections strings
        public const string TestDatabase = "data source=SCV-BD01.swiftcom.corp;"
            + "initial catalog=test_epayments;"
            + "User ID=dev;"
            + "Password=uchMeIvank;"
            + "Max Pool Size=1024;"
            + "Pooling=true;";

        // Others
        public const string DateTimeFormat = "dddd, dd MMMM yyyy HH:mm:ss.fff";
        public const string Language = "en-US";

        #endregion

#endif
    }
}