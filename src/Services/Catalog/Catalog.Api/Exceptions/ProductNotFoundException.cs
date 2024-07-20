
namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id) : base(nameof(Product), id)
        {

        }

    }
}
