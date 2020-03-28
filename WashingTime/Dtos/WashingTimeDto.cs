using System;
using System.ComponentModel.DataAnnotations;

namespace WashingTime.Dtos
{
    public class WashingTimeDto
    {
        [Required]
        public DateTime DateTime { get; set; }
    }
}