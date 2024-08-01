namespace Catalog.Api.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int id) : base(nameof(Category), id)
        {

        }

    }
}
