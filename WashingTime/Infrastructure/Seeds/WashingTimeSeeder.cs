using System.Linq;
using WashingTime.Infrastructure.Fakers;

namespace WashingTime.Infrastructure.Seeds
{
    public class WashingTimeSeeder
    {
        private readonly WashingTimeFaker _washingTimeFaker;

        private readonly WashingTimeContext _context;

        public WashingTimeSeeder(WashingTimeContext context, WashingTimeFaker washingTimeFaker)
        {
            _washingTimeFaker = washingTimeFaker;
            _context = context;
        }

        public void Seed()
        {
            if (!_context.WashingTimes.AsQueryable().Any())
            {
                _context.WashingTimes.AddRange(_washingTimeFaker.Generate(20));
            }

            _context.SaveEntitiesAsync().Wait();
        }
    }
}