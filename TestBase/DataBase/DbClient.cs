using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace TestBase.DataBase
{
    /// <summary>
    /// Sql queries client implementation
    /// </summary>
    public class DbClient : IDisposable
    {
        private string _connectionString;
        private bool _disposed;

        public DbClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Returns async entity by table key
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="id">Identifier of the table</param>
        /// <returns>Entity type of T</returns>
        public async Task<T> GetByIdentifierAsync<T>(object id) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var entity = await connection.GetAsync<T>(id).ConfigureAwait(false);

                return entity;
            }
        }

        /// <summary>
        /// Returns async all entities from the table
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns>IEnumerable of entities type of T</returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var entity = await connection.GetAllAsync<T>().ConfigureAwait(false);

                return entity;
            }
        }

        /// <summary>
        /// Insert async entity in the table
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="entity">Record type of T</param>
        /// <returns>Identifier of inserted record</returns>
        public async Task<object> InsertAsync<T>(T entity) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var identity = await connection.InsertAsync(entity).ConfigureAwait(false);

                return identity;
            }
        }

        /// <summary>
        /// Update async record in table by identifier
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="entity">Record type of T</param>
        /// <returns>Result of the query</returns>
        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var isSuccess = await connection.UpdateAsync(entity).ConfigureAwait(false);

                return isSuccess;
            }
        }

        /// <summary>
        /// Delete async record in table by identifier
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="entity">Record type of T</param>
        /// <returns>Result of the query</returns>
        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var isSuccess = await connection.DeleteAsync(entity).ConfigureAwait(false);

                return isSuccess;
            }
        }

        /// <summary>
        /// Delete async record in table
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns>Result of the query</returns>
        public async Task<bool> DeleteAllAsync<T>() where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var isSuccess = await connection.DeleteAllAsync<T>().ConfigureAwait(false);

                return isSuccess;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _connectionString = string.Empty;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}