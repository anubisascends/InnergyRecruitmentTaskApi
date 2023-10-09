using InnergyRecruitmentAPI.Config.Interfaces;
using System.Text.Json;

namespace InnergyRecruitmentAPI.Config
{
    public class PricingRetriever : IPricingRetriever
    {
        public PricingConfig Get()
        {
            var filePath = getFilePath();

            if(File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                var result = JsonSerializer.Deserialize<PricingConfig>(fileContents);

                return result!;
            }

            return null!;
        }

        private string getFilePath()
        {
            var directory = Path.GetDirectoryName(Environment.ProcessPath);

            return Path.Join(directory, "PricingData.json");
        }
    }
}
