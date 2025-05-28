
namespace TeamsAiMessager.Models
{
    public class AzureOpenAISettings
    {
        public string Endpoint { get; set; }
        public string DeploymentName { get; set; }
        public string ApiKey { get; set; }
    }

    public class GraphSettings
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string ClientSecret { get; set; }
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
    }

    public class AppSettings
    {
        public AzureOpenAISettings AzureOpenAI { get; set; }
        public GraphSettings Graph { get; set; }
    }
}
