using AutoMapper;
using InnergyRecruitmentAPI.Classes;
using InnergyRecruitmentAPI.Config;
using InnergyRecruitmentAPI.Config.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnergyRecruitmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricingController : Controller
    {
        public PricingController(IPricingRetriever pricingRetriever, IMapper mapper)
        {
            Pricing = pricingRetriever.Get();
            Mapper = mapper;
        }

        public PricingConfig Pricing { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        [Route("years")]
        public IActionResult GetYears() => Ok(Pricing.AvailableYears);

        [HttpGet]
        [Route("services")]
        public IActionResult GetServices() => Ok(Pricing.AvailableServices);

        [HttpPost]
        public IActionResult GetPrice([FromBody] PricingObject body)
        {
            var results = PricingData.Evaluate(Pricing.Pricing, body);
            var result = PricingResult.Calculate(results);

            return Ok(result);
            
        }
    }
}
