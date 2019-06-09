namespace TestBase
{
    /// <summary>
    /// Base url constants per environment
    /// </summary>
    public static class Configuration
    {
#if PRODUCTION
        #region Production environment base urls

        // Base URLs
        public const string GitHubBaseUrl = "production_environment_base_url";

        // Connections strings
        public const string TestDatabase = "production_environment_connections_string";

        #endregion

#elif SANDBOX
        #region Sandbox environment base urls

        // Base URLs
        public const string GitHubBaseUrl = "sandbox_environment_base_url";

        // Connections strings
        public const string TestDatabase = "sandbox_environment_connections_string";

        #endregion

#else

        #region Test environment base urls

        // Base URLs
        public const string GitHubBaseUrl = "https://api.github.com/";

        // Connections strings
        public const string TestDatabase = "data source=SCV-BD01.swiftcom.corp;initial catalog=test_epayments;User ID=dev;Password=uchMeIvank";

        #endregion

#endif
    }
}