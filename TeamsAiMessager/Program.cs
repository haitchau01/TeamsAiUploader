using TeamsAiMessager.Services;

var config = new ConfigService();
var openAI = new OpenAIService(config);
var auth = new GraphAuthService(config);
var json = new JsonService();

const string jsonFile = "messages.json";

var messages = await openAI.GenerateMessagesAsync(500);
json.SaveToFile(jsonFile, messages);

var token = await auth.GetAccessTokenAsync();
var teams = new TeamsService(config, token);
var loadedMessages = json.LoadFromFile<List<GraphMessage>>(jsonFile);

foreach (var msg in loadedMessages)
{
    await teams.SendMessageAsync(msg);
}