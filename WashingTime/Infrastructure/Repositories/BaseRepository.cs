using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WashingTime.Entities.Common;
using WashingTime.Exceptions;

namespace WashingTime.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        public BaseRepository(WashingTimeContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual IQueryable<T> Query => DbSet;

        public IUnitOfWork UnitOfWork => Context;

        public virtual IQueryable<T> GetQuery => Query;

        protected WashingTimeContext Context { get; }

        protected virtual DbSet<T> DbSet => Context.Set<T>();

        public virtual async Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetQuery
                       .FirstOrDefaultAsync(o => o.Id == id, cancellationToken) ??
                   throw new EntityNotFoundException();
        }

        public T Get(Guid id)
        {
            var task = GetAsync(id);
            task.Wait();
            return task.Result;
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity ?? throw new ArgumentNullException());

            return Task.CompletedTask;
        }
    }
}