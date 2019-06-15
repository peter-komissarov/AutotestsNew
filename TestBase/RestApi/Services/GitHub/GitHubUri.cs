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
        public const string Branches = Configuration.GitHubBase + "repos/aspnet/AspNetCore.Docs/branches";
    }
}