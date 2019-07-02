using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет методы для асинхронной инициализации типа Lazy.
    /// </summary>
    /// <typeparam name="T">Тип объекта, который будет асинхронно инициализирован.</typeparam>
    public sealed class AsyncLazy<T> : Lazy<Task<T>>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AsyncLazy&lt;T&gt;"/>.
        /// </summary>
        /// <param name="valueFactory">Делегат, который вызывается в фоновом потоке, для создания значения, когда это необходимо.</param>
        public AsyncLazy(Func<T> valueFactory) :
            base(() => Task.Factory.StartNew(valueFactory))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AsyncLazy&lt;T&gt;"/>.
        /// </summary>
        /// <param name="taskFactory">Асинхронный делегат, который вызывается в фоновом потоке для создания значения, когда это необходимо.</param>
        public AsyncLazy(Func<Task<T>> taskFactory) :
            base(() => Task.Factory.StartNew(taskFactory).Unwrap())
        {
        }

        /// <summary>
        /// Ожидание результата для <see cref="AsyncLazy&lt;T&gt;"/> класса.
        /// </summary>
        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}
