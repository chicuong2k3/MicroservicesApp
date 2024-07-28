
namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base(nameof(Product), id)
        {

        }

    }
}
