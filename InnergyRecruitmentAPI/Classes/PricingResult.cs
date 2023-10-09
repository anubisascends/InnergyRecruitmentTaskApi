namespace InnergyRecruitmentAPI.Classes
{
    public class PricingResult
    {
        public string Property { get; set; } = string.Empty;
        public string Operator { get; set; } = "EqualTo";
        public string Value { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public IEnumerable<PricingResult> Children { get; set; } = Array.Empty<PricingResult>();
        public bool Success { get; set; }

        public static int Calculate(IEnumerable<PricingResult> arg)
        {
            var result = 0;

            if (arg.Any())
            {
                foreach (var item in arg)
                {
                    result += Calculate(item);
                }
            }

            return result;
        }

        public static int Calculate(PricingResult arg)
        {
            var result = 0;

            if (arg.Success)
            {
                if(int.TryParse(arg.Result, out var iValue))
                {
                    result += iValue;
                }
            }

            if (arg.Success && arg.Children.Any())
            {
                foreach(var child in arg.Children)
                {
                    result += Calculate(child);
                }
            }

            return result;
        }
    }
}
