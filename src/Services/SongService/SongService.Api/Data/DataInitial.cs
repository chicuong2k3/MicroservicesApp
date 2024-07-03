using Marten.Schema;

namespace SongService.Api.Data
{
    public class DataInitial : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            var session = store.LightweightSession();

            if (await session.Query<Track>().AnyAsync())
            {
                return;
            }



            // Marten UPSERT will cater for existing records
            session.Store<Track>(GetInitialTrackData());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Track> GetInitialTrackData()
        {
            return new List<Track>
            {
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Name = "Em Không Sai Chúng Ta Sai",
                    Description = "Em Không Sai Chúng Ta Sai",
                    ThumbUrl = "",
                    CreatedAt = DateTime.Now,
                    Genres = new List<string> { "Nhạc Việt" }
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sao Anh Chưa Về Nhà",
                    Description = "Sao Anh Chưa Về Nhà",
                    ThumbUrl = "",
                    CreatedAt = DateTime.Now,
                    Genres = new List<string> { "Nhạc Việt" }
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nàng Thơ",
                    Description = "Nàng Thơ",
                    ThumbUrl = "",
                    CreatedAt = DateTime.Now,
                    Genres = new List<string> { "Nhạc Việt" }
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Name = "Buồn Thì Cứ Khóc Đi",
                    Description = "Buồn Thì Cứ Khóc Đi",
                    ThumbUrl = "",
                    CreatedAt = DateTime.Now,
                    Genres = new List<string> { "Nhạc Việt" }
                },
                new Track()
                {
                    Id = Guid.NewGuid(),
                    Name = "Anh Thanh Niên",
                    Description = "Anh Thanh Niên",
                    ThumbUrl = "",
                    CreatedAt = DateTime.Now,
                    Genres = new List<string> { "Nhạc Việt" }
                }
            };
        }
    }
}
