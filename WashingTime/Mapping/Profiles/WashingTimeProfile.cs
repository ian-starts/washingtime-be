using AutoMapper;
using WashingTime.Dtos;

namespace WashingTime.Mapping.Profiles
{
    public class WashingTimeProfile : Profile
    {
        public WashingTimeProfile()
        {
            CreateMap<WashingTimeDto, Entities.WashingTime.WashingTime>();
        }
    }
}