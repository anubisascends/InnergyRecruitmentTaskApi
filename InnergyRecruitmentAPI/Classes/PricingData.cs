using AutoMapper;
using InnergyRecruitmentAPI.Config;

namespace InnergyRecruitmentAPI.Classes
{
    public class PricingData
    {
        public string Property { get; set; } = string.Empty;
        public string Operator { get; set; } = "EqualTo";
        public string Value { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public IEnumerable<PricingData> Children { get; set; } = Array.Empty<PricingData>();

        public static IEnumerable<PricingResult> Evaluate(IEnumerable<PricingData> data, PricingObject obj)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiMapperProfile>()).CreateMapper();
            var results = mapper.Map<IEnumerable<PricingResult>>(data);

            Evaluate(results, obj);
            return results;
        }

        private static void Evaluate(IEnumerable<PricingResult> results, object obj)
        {
            if (results.Any())
            {
                foreach (var item in results)
                {
                    Evaluate(item, obj);
                }
            }
        }

        private static void Evaluate(PricingResult result, object obj)
        {
            var type = obj.GetType();
            var propertyInfo = type.GetProperty(result.Property, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var value = propertyInfo?.GetValue(obj);
            
            switch (result.Operator)
            {
                case "EqualTo":
                    result.Success = result.Value.Equals(value?.ToString() ?? string.Empty, StringComparison.OrdinalIgnoreCase);
                    break;
                case "Contains":
                    var array = value as IEnumerable<string>;
                    result.Success = array?.FirstOrDefault(x => x.Equals(result.Value, StringComparison.OrdinalIgnoreCase)) != null;
                    break;
                default:
                    break;
            }

            if (result.Success && result.Children.Any())
            {
                Evaluate(result.Children, obj);
            }
        }
    }
}
