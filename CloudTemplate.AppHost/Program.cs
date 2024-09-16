var builder = DistributedApplication.CreateBuilder(args);

// Register RabbitMQ
var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);
var messaging = builder.AddRabbitMQ("rabbitmq", username, password).WithManagementPlugin();

// Register postgres
var dataDirectory = Environment.CurrentDirectory.Replace("CloudTemplate.AppHost", "Data");
var postgres = builder.AddPostgres("postgres", username, password).WithDataBindMount("C:\\Repositories\\CloudTemplate\\Data");
var postgresdb = postgres.AddDatabase("postgresdb");

// Register Redis cache
var redis = builder.AddRedis("redis");

// Register all APIs
var productApi = builder.AddProject<Projects.Product_API>("product-api");
var orderApi = builder.AddProject<Projects.Order_API>("order-api");
var basketApi = builder.AddProject<Projects.Basket_API>("basket-api");

// Define references between resources
productApi.WithReference(basketApi).WithReference(redis).WithReference(messaging);
orderApi.WithReference(basketApi);
basketApi.WithReference(productApi).WithReference(orderApi).WithReference(messaging);

builder.Build().Run();
