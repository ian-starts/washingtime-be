using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WashingTime.Infrastructure.Repositories;

namespace WashingTime.Infrastructure
{
    public class WashingTimeContext : DbContext, IUnitOfWork
    {
        public WashingTimeContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Entities.WashingTime.WashingTime> Books { get; set; }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}