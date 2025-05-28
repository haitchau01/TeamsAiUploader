using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Runtime;
using System.Text.Json;
using TeamsAiMessager.Models;
using TeamsAiMessager.Services;

public class OpenAIService
{
    private readonly string _endpoint, _apiKey, _deployment;

    public OpenAIService(ConfigService config)
    {
        _endpoint = config.GetValue("AzureOpenAI", "Endpoint");
        _apiKey = config.GetValue("AzureOpenAI", "ApiKey");
        _deployment = config.GetValue("AzureOpenAI", "DeploymentId");
    }

    public async Task<List<GraphMessage>> GenerateMessagesAsync(int count)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

        var messages = new List<GraphMessage>();
        for (int i = 0; i < count; i++)
        {
            var prompt = "Generate a random Teams message. Include short, long messages, and some with fake file links. Output plain text.";
            var request = new
            {
                messages = new[] {
                    new { role = "system", content = "You are a Teams user." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.8,
                max_tokens = 300
            };

            var response = await httpClient.PostAsJsonAsync(
                $"{_endpoint}openai/deployments/{_deployment}/chat/completions?api-version=2024-02-01",
                request);

            var content = await response.Content.ReadFromJsonAsync<JsonElement>();
            var text = content.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            messages.Add(new GraphMessage
            {
                body = text,
                createdDateTime = DateTime.UtcNow.AddMinutes(-Random.Shared.Next(1, 100000)).ToString("o"),
                fromUserId = $"user{Random.Shared.Next(1, 10)}@contoso.com",
                attachments = new List<GraphMessage.Attachment>() { new GraphMessage.Attachment() { name = "new", contentUrl= "ad", contentType = "ádfasdf" } },
            }); 
        }

        return messages;
    }
}
