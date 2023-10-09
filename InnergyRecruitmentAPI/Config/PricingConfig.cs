using InnergyRecruitmentAPI.Classes;
using InnergyRecruitmentAPI.Config.Interfaces;

namespace InnergyRecruitmentAPI.Config
{
    public class PricingConfig
    {
        public IEnumerable<int> AvailableYears { get; set; } = Array.Empty<int>();

        public IEnumerable<string> AvailableServices {get; set; } = Array.Empty<string>();

        public IEnumerable<PricingData> Pricing {get; set;} = Array.Empty<PricingData>();
    }
}
