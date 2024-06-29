using Catalog.Api.Models;
using Common_.CQRS;

namespace Catalog.Api.Features.Products.CreateProduct
{
    public class CreateProductCommand : ICommand<CreateProductResult>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public List<string> Categories { get; set; } = new();
    }
    public class CreateProductResult
    {
        public Guid ProductId { get; set; }
    }
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = command.Name,
                Description = command.Description,
                ThumbUrl = command.ThumbUrl,
                OriginalPrice = command.OriginalPrice,
                SalePrice = command.SalePrice,
                CreatedAt = DateTime.Now
            };

            return new CreateProductResult() { ProductId = Guid.NewGuid() };
        }
    }
}
