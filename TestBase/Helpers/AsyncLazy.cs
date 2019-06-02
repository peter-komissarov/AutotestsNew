using System;
using System.Threading.Tasks;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides support for asynchronous lazy initialization.
    /// </summary>
    /// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
    public sealed class AsyncLazy<T> : Lazy<Task<T>>
    {
        /// <summary>
        /// Initializes a new instance of the lazy class. When lazy initialization occurs, the specified initialization task of function is used.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        public AsyncLazy(Func<T> valueFactory) :
            base(() => Task.Factory.StartNew(valueFactory))
        {
        }

        /// <summary>
        /// Initializes a new instance of the lazy class. When lazy initialization occurs, the specified initialization task of function is used.
        /// </summary>
        /// <param name="taskFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        public AsyncLazy(Func<Task<T>> taskFactory) :
            base(() => Task.Factory.StartNew(taskFactory).Unwrap())
        {
        }
    }
}