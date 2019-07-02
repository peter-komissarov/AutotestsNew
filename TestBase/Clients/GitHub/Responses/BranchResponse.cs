using TestBase.TestData.Poco;

namespace TestBase.Clients.GitHub.Responses
{
    /// <summary>
    /// Формат ответа от конечной точки API '/branches'.
    /// </summary>
    public class BranchResponse
    {
        /// <summary>
        /// Имя ветки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сущность для фиксации изменений программного кода в текущей ветке.
        /// </summary>
        public Commit Commit { get; set; }

        /// <summary>
        /// Флаг, указывающий является ли ветка защищенной.
        /// </summary>
        public bool Protected { get; set; }
    }
}