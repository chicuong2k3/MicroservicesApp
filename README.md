# E-commerce Application with Microservices Architecture

## Libraries used
- MediatR for CQRS pattern (a library that implements Mediator pattern).
- Mapster for Object Mapping.
- Carter for API Endpoints.
- FluentValidation for Input Validation.
- HealthChecks.* and HealthChecks.UI.Client for Health Check.
- Microsoft.FeatureManagement.AspNetCore for Feature Management.

## Authentication Service



## Catalog Service

**Architecture: Vertical Slice Architecture.**

**Patterns: CQRS.**

**Databases: PostgreSQL as a Document DB, MongoDB.**

**Libraries: Marten, MongoDB.EntityFrameworkCore.**

## Basket Service

**Architecture: Vertical Slice Architecture.**

**Patterns: CQRS, Repository, Cache-Aside, Proxy and Decorator.**

**Databases: PostgreSQL as a Document DB and Redis.**

**Libraries: Marten, StackExchangeRedis and Scrutor.**

## Discount Service

**Architecture: 3-Layer Architecture**

**Databases: SQLite**

**Libraries: Entity Framework Core, gRPC**

## Ordering Service

**Architecture: Clean Architecture.**

**Development Approach: Domain-Driven Design**

**Patterns: CQRS, Repository, Event Sourcing, REPR.**

**Databases: MSSQL Server**

**Libraries: Entity Framework Core**

## Asynchronous Communication

**Architecture: Event-Driven Microservices Architecture.**

**Patterns: Publish-Subscribe, Transactional Outbox, Saga.**

**Libraries: MassTransit, MassTransit.RabbitMQ.**

## API Gateways with YARP Reverse Proxy

**Patterns: Gateway Routing, API Gateway, Backend for Frontend.**

**Libraries: Yarp.ReverseProxy.**


## References

[A document schema for product variants and skus](https://www.elastic.co/blog/how-to-create-a-document-schema-for-product-variants-and-skus-for-your-ecommerce-search-experience)

[Modelling Products and Variants for E-Commerce](https://martinbean.dev/blog/2023/01/27/product-variants-laravel/)

[AMQP](https://en.wikipedia.org/wiki/Advanced_Message_Queuing_Protocol)

[Protocol Buffers Documentation](https://protobuf.dev/overview)

[Domain Events](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)

[Domain Events vs Integration Events](https://devblogs.microsoft.com/cesardelatorre/domain-events-vs-integration-events-in-domain-driven-design-and-microservices-architectures/)

[Dual Write Problem](https://thorben-janssen.com/dual-writes)

[Feature Flags](https://martinfowler.com/articles/feature-toggles.html)

[About MediatR and CQRS](https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs)

[Reasons for not using Auto Mapper](https://cezarypiatek.github.io/post/why-i-dont-use-automapper)

[Design Exceptions](https://cezarypiatek.github.io/post/the-art-of-designing-exceptions)

## CLI

```
dotnet new --list

dotnet new (project-type) -n (project-name) [-controllers]

dotnet dev-certs https --trust

dotnet nuget add source (path) -n (name)
```