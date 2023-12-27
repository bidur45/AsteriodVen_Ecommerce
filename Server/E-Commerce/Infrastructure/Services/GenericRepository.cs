using Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private DbContext context;
        internal DbSet<T> dbSet;
        private bool _disposed;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public IQueryable<T> All => dbSet.AsQueryable();
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default) => await dbSet.AllAsync(predicate, cancellationToken);

    public void Delete(T entity) => context.Remove(entity);

        //public void DeleteAsync(T entity) => context.Remo

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                context.Dispose();
            _disposed = true;
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{T}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public async Task<TResult> GetFirstOrDefault<TResult>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, TResult>> selector = null,
            bool disableTracking = true
        )
        {
            IQueryable<T> query = dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TResult>> GetList<TResult>(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, TResult>> selector = null,
        bool disableTracking = true
    )
        {
            IQueryable<T> query = dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else
            {
                return await query.Select(selector).ToListAsync();
            }
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

        public T GetById(object id) => dbSet.Find(id);

        public async Task<T> GetByIdAsync(object id) => await dbSet.FindAsync(id).AsTask();

        public void Insert(T entity) => dbSet.Add(entity);

        public async Task<T> InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public IQueryable<T> SqlQuery(string sql, params object[] parameters) => dbSet.FromSqlRaw(sql, parameters);
        public void Update(T entity) => dbSet.Update(entity);

        public async Task<List<T>> SqlQueryAsync(string query, List<SqlParameter> parameters = null)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters?.Count() > 0)
                {
                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        command.Parameters.Add(parameters[i]);
                    }
                }
                await context.Database.OpenConnectionAsync();
                using (var dr = await command.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                                prop.SetValue(obj, dr[prop.Name], null);
                        }
                        list.Add(obj);
                    }
                    return list;
                }
            }
        }

    }
}
