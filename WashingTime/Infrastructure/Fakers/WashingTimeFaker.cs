using System;
using Bogus;

namespace WashingTime.Infrastructure.Fakers
{
    public class WashingTimeFaker : Faker<Entities.WashingTime.WashingTime>
    {
        public WashingTimeFaker()
        {
            CustomInstantiator(f =>
            {
                return new Entities.WashingTime.WashingTime()
                {
                    UserId = "asjdhasjkdbn",
                    StartDateTime = new DateTime(),
                    EndDateTime = new DateTime()
                };
            });
        }
    }
}