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

        #endregion

#elif SANDBOX

        #region Sandbox environment constants

        // Base URLs
        public const string GitHubBaseUrl = "sandbox_environment_base_url";

        // Connections strings
        public const string TestDatabase = "sandbox_environment_connections_string";

        #endregion

#else

        #region Test environment constants

        // Base URLs
        public const string GitHubBaseUrl = "https://api.github.com/";

        // Connections strings
        public const string TestDatabase =
            "data source=SCV-BD01.swiftcom.corp;initial catalog=test_epayments;User ID=dev;Password=uchMeIvank;Max Pool Size=1024;Pooling=true;";

        #endregion

#endif
    }
}