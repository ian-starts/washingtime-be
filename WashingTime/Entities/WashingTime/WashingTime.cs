using System;
using WashingTime.Entities.Common;

namespace WashingTime.Entities.WashingTime
{
    public class WashingTime : IEntity
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public Guid UserId { get; set; }
    }
}