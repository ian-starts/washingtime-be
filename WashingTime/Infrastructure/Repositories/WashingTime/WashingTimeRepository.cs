using WashingTime.Entities.WashingTime;

namespace WashingTime.Infrastructure.Repositories.WashingTime
{
    public class WashingTimeRepository: BaseRepository<Entities.WashingTime.WashingTime>, IWashingTimeRepository
    {
        public WashingTimeRepository(WashingTimeContext context)
            : base(context)
        {
        }
    }
}