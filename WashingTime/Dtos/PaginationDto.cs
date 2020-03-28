using Microsoft.AspNetCore.Mvc;

namespace WashingTime.Dtos
{
    public class PaginationDto
    {
        [FromQuery(Name = "page[number]")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "page[size]")]
        public int PageSize { get; set; } = 10;
    }
}