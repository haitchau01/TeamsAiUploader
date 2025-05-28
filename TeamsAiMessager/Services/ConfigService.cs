using Microsoft.Extensions.Configuration;
namespace TeamsAiMessager.Services
{
    public class ConfigService
    {
        public IConfiguration Configuration { get; }

        public ConfigService()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public string GetValue(string section, string key) =>
            Configuration.GetSection(section)[key];
    }
}
