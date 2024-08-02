using Catalog.Api.Data.Dtos;
using Catalog.Api.Models;

namespace Catalog.Api.Extensions
{
    public static class DtoExtensions
    {
        public static ProductDto ToProductDto(this Product product)
        {
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Slug = product.Slug,
                ThumbnailUrl = product.ThumbnailUrl,
                CategoryId = product.CategoryId
            };

            if (product.Variants != null)
            {
                var variantDtos = new List<VariantDto>();

                foreach (var variant in product.Variants)
                {
                    
                    var attributeDtos = new List<AttributeDto>();

                    if (variant.VariantOptions != null)
                    {
                        foreach (var variantOption in variant.VariantOptions)
                        {
                            attributeDtos.Add(new AttributeDto()
                            {
                                Name = variantOption.ProductAttribute!.Name,
                                Value = variantOption.Value
                            });
                        }
                    }

                    var variantDto = new VariantDto()
                    {
                        Sku = variant.Sku,
                        StockQuantity = variant.StockQuantity,
                        Price = variant.Price,
                        ImageUrl = variant.ImageUrl
                    };

                    variantDto.Attributes = attributeDtos;

                    variantDtos.Add(variantDto);
                }

                productDto.Variants = variantDtos;

            }

            return productDto;
        }
    }
}
