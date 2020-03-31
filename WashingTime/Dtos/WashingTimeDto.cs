using System;
using System.ComponentModel.DataAnnotations;
using Type = WashingTime.Entities.WashingTime.Enums.Type;

namespace WashingTime.Dtos
{
    public class WashingTimeDto
    {
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
        
        [Required]
        public Type WasherType { get; set; }
    }
}