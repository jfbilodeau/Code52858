using Azure.Identity;
using Microsoft.Graph;

Console.WriteLine("Starting GraphDemo...");

var clientId = "41725e58-9f23-4dbf-b4e0-6768accb7251";
var tenantId = "14f08b43-3b8c-4f1c-87c5-71e2bb2177f3";

// var credentials = new DefaultAzureCredential();
// var credentials = new DeviceCodeCredential();
var credentials = new InteractiveBrowserCredential(tenantId, clientId);

var graphClient = new GraphServiceClient(credentials);

var stream = await graphClient.Me.Photo.Content.GetAsync();

using var fileStream = File.Create("profile.png");

await stream.CopyToAsync(fileStream);

Console.WriteLine("Done!");