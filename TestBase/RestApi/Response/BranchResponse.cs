using TestBase.Data.Poco;

namespace TestBase.RestApi.Response
{
    public class BranchResponse
    {
        public string Name { get; set; }

        public Commit Commit { get; set; }

        public bool Protected { get; set; }
    }
}