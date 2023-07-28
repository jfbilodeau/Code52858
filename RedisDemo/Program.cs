using StackExchange.Redis;

Console.WriteLine("Azure Cache for Redis Demo");

var connectionString = "jfredisdemo.redis.cache.windows.net:6380,password=pDR5OiPFJOr895tmVXftB5AoTBz88hIgCAzCaNDnYlQ=,ssl=True,abortConnect=False";

using var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);

var database = connection.GetDatabase();

var latency = database.Ping();

Console.WriteLine($"Latency: {latency.TotalMilliseconds}ms");

var key = "jfwebapp:employees:list";
var value = "[{\"id\":1,\"firstName\":\"John\",\"lastName\":\"Smith\",\"email\":";
var ttl = TimeSpan.FromSeconds(5);

var result = await database.StringSetAsync(key, value, ttl);

if (result)
{
    Console.WriteLine("Key stored successfully!");
} else {
    Console.WriteLine("Key failed to store!");
}

var retrievedValue = await database.StringGetAsync(key);

Console.WriteLine($"Retrieved value: {retrievedValue}");

Console.WriteLine("Sleeping for 6 seconds...");

await Task.Delay(6000);

retrievedValue = await database.StringGetAsync(key);

Console.WriteLine($"Retrieved value: {retrievedValue}");

Console.WriteLine("Done!");