using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TeamsAiMessager.Services
{
    public class TeamsService
    {
        private readonly string _teamId, _channelId;
        private readonly HttpClient _http;

        public TeamsService(ConfigService config, string token)
        {
            _teamId = config.GetValue("Graph", "TeamId");
            _channelId = config.GetValue("Graph", "ChannelId");
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task SendMessageAsync(GraphMessage message)
        {
            var payload = new
            {
                createdDateTime = message.createdDateTime,
                from = new
                {
                    user = new { id = message.fromUserId }
                },
                body = new
                {
                    contentType = "html",
                    content = message.body
                },
                attachments = message.attachments.Select(att => new
                {
                    contentType = "reference",
                    contentUrl = att,
                    name = Path.GetFileName(att.ToString())
                })
            };

            var url = $"https://graph.microsoft.com/v1.0/teams/{_teamId}/channels/{_channelId}/messages";

            var response = await _http.PostAsJsonAsync(url, payload);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send: {error}");
            }
        }
    }
}
