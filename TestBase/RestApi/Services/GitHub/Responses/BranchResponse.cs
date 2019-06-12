using TestBase.Data.PocoClasses;

namespace TestBase.RestApi.Services.GitHub.Responses
{
    public class BranchResponse
    {
        public string Name { get; set; }

        public Commit Commit { get; set; }

        public bool Protected { get; set; }
    }
}