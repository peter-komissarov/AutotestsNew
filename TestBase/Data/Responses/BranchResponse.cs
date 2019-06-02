using TestBase.Data.Pocos;

namespace TestBase.Data.Responses
{
    public class BranchResponse
    {
        public string Name { get; set; }

        public Commit Commit { get; set; }

        public bool Protected { get; set; }
    }
}