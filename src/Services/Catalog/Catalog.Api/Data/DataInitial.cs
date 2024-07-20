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
                    Id = random.Next(100),
                    Name = "Nhập môn lập trình",
                    Description = "Nhập môn lập trình"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Kỹ thuật lập trình",
                    Description = "Kỹ thuật lập trình"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Giải tích 1",
                    Description = "Giải tích 1"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Giải tích 2",
                    Description = "Giải tích 2"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Vật lý đại cương 1",
                    Description = "Vật lý đại cương 1"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Vật lý đại cương 2",
                    Description = "Vật lý đại cương 2"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Triết học",
                    Description = "Triết học"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Chủ nghĩa xã hội khoa học",
                    Description = "Chủ nghĩa xã hội khoa học"
                },
                new Product()
                {
                    Id = random.Next(100),
                    Name = "Tư tưởng Hồ Chí Minh",
                    Description = "Tư tưởng Hồ Chí Minh"
                }
            };
        }
    }
}
