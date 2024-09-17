var builder = DistributedApplication.CreateBuilder(args);

// Default username and password
var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

// Register RabbitMQ
var messaging = builder.AddRabbitMQ("rabbitmq", username, password).WithManagementPlugin();

// Register postgres
var dataDirectory = Environment.CurrentDirectory.Replace("CloudTemplate.AppHost", "Data");
var postgres = builder.AddPostgres("postgres", username, password).WithPgAdmin().WithDataBindMount(dataDirectory);
var product_db = postgres.AddDatabase("productdb", "Products");
var basket_db = postgres.AddDatabase("basketdb", "Baskets");
var order_db = postgres.AddDatabase("orderdb", "Orders");

// Register Redis cache
var redis = builder.AddRedis("redis");

// Register all APIs
var productApi = builder.AddProject<Projects.Product_API>("product-api");
var orderApi = builder.AddProject<Projects.Order_API>("order-api");
var basketApi = builder.AddProject<Projects.Basket_API>("basket-api");

// Define references between resources
productApi.WithReference(basketApi).WithReference(redis).WithReference(messaging).WithReference(product_db);
orderApi.WithReference(basketApi).WithReference(order_db);
basketApi.WithReference(productApi).WithReference(orderApi).WithReference(messaging).WithReference(basket_db);

builder.Build().Run();
