using Microsoft.Azure.Cosmos;

Console.WriteLine("Cosmos DB Example");

var uri = "https://jfbdemo.documents.azure.com:443/";
var key = "hKcQganLYr0lLeOP8wmEuW5UYvxaJmFbKtM526uo94sNHPJBNwl5u2MWcQt2XGcagH8SgXidga3TACDblQcHAw==";

var client = new CosmosClient(
    uri,
    key,
    new CosmosClientOptions()
    {
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    }
);

var databaseName = "hr";
var containerName = "employees";

var container = client.GetContainer(databaseName, containerName);


// Create an employee
var newEmployee = new Employee
{
    Id = Guid.NewGuid().ToString(),
    FirstName = "John",
    LastName = "Doe",
    Department = "Sales",
    Salary = 50000
};

var response = await container.CreateItemAsync(newEmployee);

Console.WriteLine($"Status code: {response.StatusCode}");


var query = new QueryDefinition("SELECT * FROM employees e");

var iterator = container.GetItemQueryIterator<Employee>(query);

while (iterator.HasMoreResults)
{
    var results = await iterator.ReadNextAsync();

    foreach (var employee in results)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Department} - {employee.Salary}");
    }
}


Console.WriteLine("Done!");