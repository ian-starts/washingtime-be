using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WashingTime.QueryExtensions.Include
{
    public static class Includer
    {
        public static IQueryable<TEntity> IncludeMany<TEntity>(
            this IQueryable<TEntity> source,
            string navigationPropertyPaths)
            where TEntity : class
        {
            return navigationPropertyPaths?.Split(char.Parse(","))
                .Aggregate(
                    source,
                    (current, path) =>
                        current.Include(path.Trim())) ?? source;
        }
    }
}