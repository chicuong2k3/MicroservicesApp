using Marten.Schema;

namespace Catalog.Api.Data
{
    public class DataInitial : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
            {
                return;
            }



            // Marten UPSERT will cater for existing records
            session.Store(GetInitialTrackData());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetInitialTrackData()
        {
            var random = new Random();
            return new List<Product>
            {
                new Product()
                {
                    Id = new Guid("6267e3aa-3e20-4600-8cf8-ae908a55eb30"),
                    Name = "Quần Kaki Nam Premium Ống Ôm Trơn Form Slim Cropped",
                    Description = "Quần Kaki Nam Premium Ống Ôm Trơn Form Slim Cropped",
                    Variants = new List<Variant>()
                    {
                        new Variant()
                        {
                            Id = 1,
                            Sku = "QKNB28",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "28"}
                            }
                        },
                        new Variant()
                        {
                            Id = 2,
                            Sku = "QKNB29",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "29"}
                            }
                        },
                        new Variant()
                        {
                            Id = 3,
                            Sku = "QKNB30",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "30"}
                            }
                        },
                        new Variant()
                        {
                            Id = 4,
                            Sku = "QKNT28",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "28"}
                            }
                        },
                        new Variant()
                        {
                            Id = 5,
                            Sku = "QKNT29",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "29"}
                            }
                        },
                        new Variant()
                        {
                            Id = 6,
                            Sku = "QKNT30",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "30"}
                            }
                        }
                    }
                },
                new Product()
                {
                    Id = new Guid("5853fe3d-677c-4bce-aa27-d12bf2b45e2e"),
                    Name = "Quần Cargo Nam Túi Hộp Ống Rộng Trơn Form Wide Leg",
                    Description = "Quần Cargo Nam Túi Hộp Ống Rộng Trơn Form Wide Leg",
                    Variants = new List<Variant>()
                    {
                        new Variant()
                        {
                            Id = 1,
                            Sku = "QKNBT28",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "28"}
                            }
                        },
                        new Variant()
                        {
                            Id = 2,
                            Sku = "QKNBT29",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "29"}
                            }
                        },
                        new Variant()
                        {
                            Id = 3,
                            Sku = "QKNBT30",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Black"},
                                new VariantOption() { Name = "Size", Value = "30"}
                            }
                        },
                        new Variant()
                        {
                            Id = 4,
                            Sku = "QKNTT28",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "28"}
                            }
                        },
                        new Variant()
                        {
                            Id = 5,
                            Sku = "QKNTT29",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "29"}
                            }
                        },
                        new Variant()
                        {
                            Id = 6,
                            Sku = "QKNTT30",
                            StockQuantity = random.Next(10),
                            Price = 638000,
                            Options = new List<VariantOption>()
                            {
                                new VariantOption() { Name = "Color", Value = "Tofu"},
                                new VariantOption() { Name = "Size", Value = "30"}
                            }
                        }
                    }
                }
            };
        }
    }
}
