using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> SqlQuery(string sql, params object[] parameters);
        // Task<List<T>> SqlQueryAsync(string query, List<SqlParameter> parameters = null);
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        IQueryable<T> All { get; }

        public Task<TResult> GetFirstOrDefault<TResult>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, TResult>> selector = null,
            bool disableTracking = true
        );
        public Task<List<TResult>> GetList<TResult>(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, TResult>> selector = null,
        bool disableTracking = true
    );
        IQueryable<T> GetAll
            (
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes
            );
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        void Insert(T entity);
        Task<T> InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<List<T>> SqlQueryAsync(string query, List<SqlParameter> parameters = null);
    }
}
