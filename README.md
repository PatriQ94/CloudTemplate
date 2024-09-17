

<h2 align="center">.NET 8 microservices reference implementation</h2>

<p align="center">
  <a href="#about">About</a> ◈
  <a href="#architecture">Architecture</a> ◈
  <a href="#prerequisites">Prerequisites</a> ◈
  <a href="#build-and-run">Build and run</a> ◈
  <a href="#roadmap">Roadmap</a>
</p>


## About

This project showcases one of many implementations of [microservices](https://microservices.io/) in .NET 8. 
It also includes a variety of different tools and technologies such as [PostgreSQL](https://www.postgresql.org/) for data storage, 
[EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/) as the ORM, [Redis](https://redis.io/) for caching, 
[RabbitMQ](https://www.rabbitmq.com/) with [MassTransit](https://masstransit.io/) for the event based communication between microservices,
[Serilog](https://serilog.net/) for logging, [Moq](https://github.com/devlooped/moq) for unit testing, API gateways and
[.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) to run the entire application.

**The main purpose of the project is to be used as a reference implementation in
case of need.**

These docs are work in progress :)

## Architecture
TBD

## Prerequisites
TBD

## Build and run
TBD

## Roadmap

- [x] Initial system architecture
- [x] .NET Aspire configured
- [x] Product API architecture
- [x] Basket API architecture
- [ ] Order API architecture
- [ ] API gateway implemented
- [ ] Redis caching implemented
- [x] MassTransit configured
- [x] RabbitMQ integrated
- [x] Serilog logging
- [ ] PostgreSQL integrated
- [ ] EntityFramework configured
- [ ] TBD, many many more