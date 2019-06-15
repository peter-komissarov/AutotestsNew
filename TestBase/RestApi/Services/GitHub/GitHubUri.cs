namespace TestBase.RestApi.Services.GitHub
{
    /// <summary>
    /// Абсолютные Uri для сервиса GitHub.
    /// </summary>
    public static class GitHubUri
    {
        /// <summary>
        /// Uri для получения списка веток.
        /// </summary>
        public const string GitHubBranches = Configuration.GitHubBase + "repos/aspnet/AspNetCore.Docs/branches";
    }
}