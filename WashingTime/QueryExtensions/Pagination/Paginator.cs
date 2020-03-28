using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WashingTime.QueryExtensions.Pagination
{
    public static class Paginator
    {
        public static PaginatedResult<T> Paginate<T>(
            this IQueryable<T> query,
            int page = 1,
            int pageSize = 10)
            where T : class
        {
            var list = query.PaginateQuery<T>(page, pageSize).ToList<T>();
            return new PaginatedResult<T>(page, pageSize, query.Count<T>(), list);
        }

        public static async Task<PaginatedResult<T>> PaginateAsync<T>(
            this IQueryable<T> query,
            int page = 1,
            int pageSize = 10)
            where T : class
        {
            var data = await query.PaginateQuery(page, pageSize).ToListAsync();
            return new PaginatedResult<T>(page, pageSize, query.Count(), data);
        }

        private static IQueryable<T> PaginateQuery<T>(
            this IQueryable<T> query,
            int page = 1,
            int pageSize = 10)
            where T : class
        {
            var count = (page - 1) * pageSize;
            return query.Skip(count).Take(pageSize);
        }
    }
}