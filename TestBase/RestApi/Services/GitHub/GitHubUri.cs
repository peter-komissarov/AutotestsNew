using TestBase.Helpers;

namespace TestBase.RestApi.Services.GitHub
{
    /// <summary>
    /// Uri для сервиса GitHub.
    /// </summary>
    public static class GitHubUri
    {
        /// <summary>
        /// Uri для получения списка веток.
        /// </summary>
        public static readonly string Branches = ConfigurationHelper.TestConfig["HostNames:GitHub"] + "repos/aspnet/AspNetCore.Docs/branches";
    }
}