using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDbContext: IDisposable
    {
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
    where TEntity : new();

        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
            where TElement : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        int Commit();
        void Rollback();
        Task<int> CommitAsync();

    }
}
