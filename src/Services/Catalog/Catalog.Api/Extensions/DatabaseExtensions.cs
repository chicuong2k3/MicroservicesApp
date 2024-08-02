using Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static void InitializeDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();


                SeedDatabase(context);
            }
        }

        private static void SeedDatabase(AppDbContext context)
        {

            
            if (!context.Categories.Any())
            {
                var productCategoryCombination = DataCrawler.GetProductsCategory(20).Result;

                var categories = new List<Category>();
                foreach (var combination in productCategoryCombination)
                {
                    var exist = categories.Any(x => x.Name.Equals(combination.Item2.Name));

                    if (!exist)
                    {
                        categories.Add(combination.Item2);
                    }
                    
                }

                context.Categories.AddRange(categories);

                context.SaveChanges();

                if (!context.Products.Any())
                {

                    var colorAttr = context.ProductAttributes.FirstOrDefault(x => x.Name.Equals("Color"));
                    var sizeAttr = context.ProductAttributes.FirstOrDefault(x => x.Name.Equals("Size"));

                    if (colorAttr == null) colorAttr = new ProductAttribute() { Name = "Color" };
                    if (sizeAttr == null) sizeAttr = new ProductAttribute() { Name = "Size" };

                    foreach (var combination in productCategoryCombination)
                    {
                        foreach (var variant in combination.Item1.Variants)
                        {
                            variant.VariantOptions[0].ProductAttribute = colorAttr;
                            variant.VariantOptions[1].ProductAttribute = sizeAttr;
                        }

                        var category = context.Categories.FirstOrDefault(x => x.Name.Equals(combination.Item2.Name));

                        combination.Item1.Category = category;

                        context.Products.Add(combination.Item1);
                    }

                    context.SaveChanges();

                }
            }


            

            
        }
    }
}
