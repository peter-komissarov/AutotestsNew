namespace TestBase.RestApi.Url
{
    /// <summary>
    /// Base url constants per environment
    /// </summary>
    public static class BaseUrl
    {
#if PRODUCTION

        #region Production environment base urls

        public const string GitHubBaseUrl = "production_environment_base_url";

        #endregion

#elif SANDBOX

        #region Sandbox environment base urls

        public const string GitHubBaseUrl = "sandbox_environment_base_url";

        #endregion

#else

        #region Test environment base urls

        public const string GitHubBaseUrl = "https://api.github.com/";

        #endregion

#endif
    }
}
