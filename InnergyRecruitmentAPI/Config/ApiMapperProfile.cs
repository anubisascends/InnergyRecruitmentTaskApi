using AutoMapper;
using InnergyRecruitmentAPI.Classes;

namespace InnergyRecruitmentAPI.Config
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<PricingData, PricingResult>();
        }
    }
}
