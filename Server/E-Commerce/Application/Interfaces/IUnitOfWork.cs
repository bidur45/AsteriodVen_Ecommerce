using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public void BeginTransaction();
        public int Commit();
        public Task<int> CommitAsync();
        public void Dispose();
        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        public void Rollback();
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
