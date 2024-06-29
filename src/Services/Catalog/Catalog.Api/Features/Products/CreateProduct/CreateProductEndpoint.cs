

namespace Catalog.Api.Features.Products.CreateProduct
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public List<string> Categories { get; set; } = new();
    }
    public class CreateProductResponse
    {
        public Guid ProductId { get; set; }
    }
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.ProductId}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product");
        }
    }
}
