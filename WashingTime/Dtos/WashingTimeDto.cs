using System;
using System.ComponentModel.DataAnnotations;

namespace WashingTime.Dtos
{
    public class WashingTimeDto
    {
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}