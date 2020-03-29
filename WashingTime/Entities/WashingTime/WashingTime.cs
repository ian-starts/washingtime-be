using System;
using WashingTime.Entities.Common;

namespace WashingTime.Entities.WashingTime
{
    public class WashingTime : IEntity
    {
        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }
        
        public DateTime EndDateTime { get; set; }

        public string UserId { get; set; }
    }
}