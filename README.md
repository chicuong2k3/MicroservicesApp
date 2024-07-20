# E-commerce Application using Microservices Architecture

## Common libraries used in all Microservices
- MediatR for CQRS pattern (a library that implements Mediator pattern).
- Mapster for Object Mapping.
- Carter for API Endpoints.
- FluentValidation for Input Validation.
- HealthChecks.* and HealthChecks.UI.Client for Health Check.

## Catalog Service

**Architectures: Vertical Slice, CQRS.**

**Databases: PostgreSQL as a Document DB.**

**Libraries: Marten**

## Basket Service

**Architectures: Vertical Slice, CQRS.**

**Patterns: Repository, Cache-Aside, Proxy and Decorator.**

**Databases: PostgreSQL as a Document DB and Redis.**

**Libraries: Marten, StackExchangeRedis and Scrutor.**

## References

[A document schema for product variants and skus](https://www.elastic.co/blog/how-to-create-a-document-schema-for-product-variants-and-skus-for-your-ecommerce-search-experience)

[Modelling Products and Variants for E-Commerce](https://martinbean.dev/blog/2023/01/27/product-variants-laravel/)

