using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WashingTime.Infrastructure.Filters
{
    public static class WashingTimeFilter
    {
        public static IQueryable<Entities.WashingTime.WashingTime> ApplyFilters(
            this IQueryable<Entities.WashingTime.WashingTime> query,
            Dtos.Filters.WashingTimeFilter filters)
        {
            if (filters.DateGte != null)
            {
                query = query
                    .Where(
                        o => o.StartDateTime > filters.DateGte);
            }

            return query;
        }
    }
}