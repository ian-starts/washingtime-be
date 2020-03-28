using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WashingTime.Entities.Common;

namespace WashingTime.Infrastructure.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        IQueryable<T> Query { get; }
        
        public IUnitOfWork UnitOfWork { get; }
        
        T Get(Guid id);

        void Update(T entity);

        void Add(T entity);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}