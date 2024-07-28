using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;


namespace Ordering.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static bool HasChangedOwnEntities(this EntityEntry entity)
        {
            return entity.References.Any(x =>
            {
                return x.TargetEntry != null && x.TargetEntry.Metadata.IsOwned()
                && (
                    x.TargetEntry.State == EntityState.Added ||
                    x.TargetEntry.State == EntityState.Modified
                );
            });
        }
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

            var customerId1 = CustomerId.Generate(new Guid("6e59dfb5-a6d1-4ab4-95a9-04e6b8390712"));
            var customerId2 = CustomerId.Generate(new Guid("ceddab73-47a5-4bb0-9332-7e2ed837498a"));
            var customerId3 = CustomerId.Generate(new Guid("9276021b-85e9-4c58-b346-cdd8e11e81da"));

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(new List<Customer>()
                {
                    Customer.Create(customerId1, "trandung123", "Dũng", "Trần", "dung123@gmail.com"),
                    Customer.Create(customerId2, "nguyentuan123", "Tuấn", "Nguyễn", "tuan123@gmail.com"),
                    Customer.Create(customerId3, "tranphuong456", "Phương", "Trần", "phuong123@gmail.com")
                });

            }

            var productId1 = ProductId.Generate(new Guid("7d976fa9-22cc-4655-afce-87ed811f0616"));
            var productId2 = ProductId.Generate(new Guid("bcd91d21-3d99-4c84-b1a4-5df6d35f7d19"));
            var productId3 = ProductId.Generate(new Guid("a9aae3d4-bc23-42b5-8a53-ce95a319e514"));

            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    Product.Create(productId1, "Iphone X", 400),
                    Product.Create(productId2, "Laptop Acer", 1200),
                    Product.Create(productId3, "Bàn chải đánh răng", 50)
                });

            }

            if (!context.Orders.Any())
            {
                var order1 = Order.Create(
                        OrderId.Generate(Guid.NewGuid()),
                        customerId1, OrderName.Generate("Order 1"),
                        Address.Generate("HCM", "Thủ Đức", "Bình Chiểu", "Thủ Đức, HCM"),
                        Payment.Generate("Thanh toán order 1", 2));

                order1.AddOrderItem(productId1, 1, 400, 2);
                order1.AddOrderItem(productId2, 1, 1200, 1);

                var order2 = Order.Create(
                        OrderId.Generate(Guid.NewGuid()),
                        customerId1, OrderName.Generate("Order 2"),
                        Address.Generate("HCM", "Tân Bình", "P2", "Tân Bình, HCM"),
                        Payment.Generate("Thanh toán order 2", 1));

                order2.AddOrderItem(productId3, 1, 50, 4);

                context.Orders.AddRange(new List<Order>()
                {
                    order1, order2
                });


            }

            context.SaveChanges();
        }
    }
}
