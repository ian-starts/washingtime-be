using System.Collections.Generic;

namespace WashingTime.QueryExtensions.Pagination
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(int currentPage, int pageSize, int total, IEnumerable<T> data)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int Total { get; private set; }

        public IEnumerable<T> Data { get; private set; }
    }
}