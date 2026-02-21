V3 - Microservices with Ocelot + Kafka (NET 8 / C# 12)
This skeleton provides 3 microservices (customers, catalog, orders) and an Ocelot API gateway.
It demonstrates an event-driven architecture skeleton (Kafka producer/consumer placeholders), Ocelot routing,
and service decomposition. Each microservice is a Minimal API with EF InMemory for dev, Swagger, AutoMapper and FluentValidation.

Quickstart (in-memory):
  dotnet restore
  dotnet build
  USE_INMEMORY=true dotnet run --project src/customers/customers.csproj
  USE_INMEMORY=true dotnet run --project src/catalog/catalog.csproj
  USE_INMEMORY=true dotnet run --project src/orders/orders.csproj
  dotnet run --project src/gateway/Gateway.csproj
  Open gateway at http://localhost:7000

Docker-compose (dev with Kafka & Zookeeper):
  docker-compose up -d
  # services will be available on configured ports
