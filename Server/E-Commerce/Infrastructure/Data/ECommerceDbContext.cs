using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.ECommerce;
using Domain.Entities.Security;
using Domain.Seeds.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ECommerceDbContext : DbContext, IDbContext
    {
        private DbTransaction? _transaction;
        private ICurrentUserService _currentUserService;
        public ECommerceDbContext(
            DbContextOptions<ECommerceDbContext> options,
            ICurrentUserService currentUserService
        ) : base(options)
        {
            _currentUserService = currentUserService;
        }

        //Security
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
   .HasOne(u => u.EntryBy)
   .WithMany()
   .HasForeignKey(u => u.EntryById)
   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(u => u.UpdatedBy)
                .WithMany()
                .HasForeignKey(u => u.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>()
               .HasData(SecuritySeed.Users);
        }

        public void BeginTransaction()
        {
            if (Database.GetDbConnection().State
                == ConnectionState.Open)
            {
                return;
            }
            Database.GetDbConnection().Open();
            _transaction = Database.GetDbConnection().BeginTransaction();
        }

        public int Commit()
        {
            var saveChanges = SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
            => base.Set<TEntity>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.EntryById = _currentUserService.UserId; 
                        entry.Entity.EntryDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property(nameof(entry.Entity.EntryById)).IsModified = false;
                        entry.Property(nameof(entry.Entity.EntryDate)).IsModified = false;
                        entry.Entity.UpdatedById = _currentUserService.UserId; 
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters) where TElement : class
            => base.Set<TElement>().FromSqlRaw(sql, parameters).AsEnumerable();

    }
}
