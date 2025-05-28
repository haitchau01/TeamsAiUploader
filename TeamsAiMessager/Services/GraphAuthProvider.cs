using Azure.Core;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Azure.Identity;
namespace TeamsAiMessager.Services
{
    public class GraphAuthService
    {
        private readonly string _clientId, _secret, _tenant;

        public GraphAuthService(ConfigService config)
        {
            _clientId = config.GetValue("AzureAd", "ClientId");
            _secret = config.GetValue("AzureAd", "ClientSecret");
            _tenant = config.GetValue("AzureAd", "TenantId");
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var options = new TokenCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };

            var credential = new ClientSecretCredential(_tenant, _clientId, _secret, options);
            var token = await credential.GetTokenAsync(new TokenRequestContext(scopes));
            return token.Token;
        }
    }
}
