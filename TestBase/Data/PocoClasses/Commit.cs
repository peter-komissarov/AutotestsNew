using System;

namespace TestBase.Data.PocoClasses
{
    /// <summary>
    /// Сущность для фиксации изменений программного кода в текущей ветке.
    /// </summary>
    public class Commit
    {
        /// <summary>
        /// Хэш.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Унифицированный идентификатор ресурса.
        /// </summary>
        public Uri Url { get; set; }
    }
}