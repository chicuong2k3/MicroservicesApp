# E-commerce Application with Microservices Architecture

## Libraries used
- MediatR for CQRS pattern (a library that implements Mediator pattern).
- Mapster for Object Mapping.
- Carter for API Endpoints.
- FluentValidation for Input Validation.
- HealthChecks.* and HealthChecks.UI.Client for Health Check.

## Catalog Service

**Architecture: Vertical Slice Architecture.**

**Patterns: CQRS.**

**Databases: PostgreSQL as a Document DB.**

**Libraries: Marten**

## Basket Service

**Architecture: Vertical Slice Architecture.**

**Patterns: CQRS, Repository, Cache-Aside, Proxy and Decorator.**

**Databases: PostgreSQL as a Document DB and Redis.**

**Libraries: Marten, StackExchangeRedis and Scrutor.**

## Discout Service

**Architecture: 3-Layer Architecture**

**Databases: SQLite**

**Libraries: Entity Framework Core, gRPC**

## Ordering Service

**Architecture: Clean Architecture.**

**Development Approach: Domain-Driven Design**

**Patterns: CQRS, Repository, Event Sourcing, REPR**

**Databases: MSSQL Server**

**Libraries: Entity Framework Core, MassTransit**

## References

[A document schema for product variants and skus](https://www.elastic.co/blog/how-to-create-a-document-schema-for-product-variants-and-skus-for-your-ecommerce-search-experience)

[Modelling Products and Variants for E-Commerce](https://martinbean.dev/blog/2023/01/27/product-variants-laravel/)

[AMQP](https://en.wikipedia.org/wiki/Advanced_Message_Queuing_Protocol)

[Protocol Buffers Documentation](https://protobuf.dev/overview)
