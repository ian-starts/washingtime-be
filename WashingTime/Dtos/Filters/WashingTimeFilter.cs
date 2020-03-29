using System;
using Microsoft.AspNetCore.Mvc;

namespace WashingTime.Dtos.Filters
{
    public class WashingTimeFilter
    {
        [FromQuery(Name = "filter[dateGte]")]
        public DateTime? DateGte { get; set; }
    }
}