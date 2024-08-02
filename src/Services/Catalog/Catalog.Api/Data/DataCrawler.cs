using Newtonsoft.Json;
using System.Text;
using HtmlAgilityPack;

namespace Catalog.Api.Data
{
    public static class DataCrawler
    {
        private static async Task<string> GetHtml(string url)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<List<(Product, Category)>> GetProductsCategory(int productNumber)
        {
            List<(string, string)> combination = await GetProductUrlsCategoryCombination(productNumber);

            var categoryList = new List<Category>();

            var result = new List<(Product, Category)>();

            foreach (var (url, categoryUrl) in combination)
            {
                var category = await GetCategory(categoryUrl);


                var html = await GetHtml(url);
                var htmlDoc = new HtmlDocument();
                htmlDoc.OptionDefaultStreamEncoding = Encoding.UTF8;
                htmlDoc.LoadHtml(html);

                var productNode = htmlDoc.DocumentNode.Descendants("script")
                    .FirstOrDefault(node => node.GetAttributeValue("type", "").Equals("application/ld+json"));

                var jsonString = productNode!.InnerHtml;
                var parsedProduct = JsonConvert.DeserializeObject<ProductParseModel>(jsonString);

                if (parsedProduct != null)
                {
                    result.Add((new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = parsedProduct.Name ?? string.Empty,
                        Description = parsedProduct.Description,
                        ThumbnailUrl = parsedProduct.Image ?? string.Empty,
                        Variants = parsedProduct.Offers != null && parsedProduct.Offers.OfferList != null ?
                        parsedProduct.Offers.OfferList.Select(x =>
                        {
                            var parts = x.Name!.Split(" ");

                            return new Variant()
                            {
                                Sku = x.Sku ?? string.Empty,
                                StockQuantity = new Random().Next(20),
                                Price = x.Price,
                                ImageUrl = x.Image,
                                VariantOptions = new List<VariantOption>
                                {
                                    new VariantOption()
                                    {
                                        Value = parts[1]
                                    },
                                    new VariantOption()
                                    {
                                        Value = parts[2]
                                    }
                                }
                            };
                        }).ToList() : []
                    }, category));
                }
            }

            return result;
        }

        private static async Task<List<(string, string)>> GetProductUrlsCategoryCombination(int productNumber)
        {
            var result = new List<(string, string)>();
            foreach (var url in await GetCategoryUrls())
            {
                int p = 1;

                while (true)
                {
                    var temp = await GetProductsInAPage(url + $"?p={p}");
                    if (temp.Count <= 4)
                    {
                        break;
                    }

                    foreach (var x in temp)
                    {
                        result.Add((x, url));
                        if (result.Count >= productNumber)
                        {
                            return result;
                        }
                    }

                    p++;
                }
            }

            return result;
        }

        private static async Task<List<string>> GetProductsInAPage(string url)
        {
            var html = await GetHtml(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var categoryItems = htmlDoc.DocumentNode.Descendants("strong")
                .Where(node => node.GetAttributeValue("class", "").Contains("product-item-name"))
                .ToList();

            var links = new List<string>();

            foreach (var item in categoryItems)
            {
                var href = item.FirstChild.GetAttributeValue("href", null);
                if (href != null)
                {
                    links.Add(href);
                }
            }

            return links;
        }

        public static async Task<List<string>> GetCategoryUrls()
        {
            var html = await GetHtml("https://routine.vn");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var categoryItems = htmlDoc.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "").Equals("itemsubmenu"))
                .FirstOrDefault()
                ?.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("itemMenu")
                && node.ChildNodes.Count == 2)
                .ToList();

            var links = new List<string>();

            if (categoryItems != null)
            {
                foreach (var item in categoryItems)
                {
                    var href = item.FirstChild.GetAttributeValue("href", null);
                    if (href != null)
                    {
                        links.Add(href);
                    }
                }
            }

            return links;
        }

        private static async Task<Category> GetCategory(string url)
        {
            var categoryNames = new Queue<string>();

            var html = await GetHtml(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var categoryNodes = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("item category"))
                .ToList();

            foreach (var node in categoryNodes)
            {
                categoryNames.Enqueue(node.ChildNodes[1].InnerHtml);
            }

            Category? pCategory = null;
            Category category = new Category();

            while (categoryNames.Count > 0)
            {
                category = new Category()
                {
                    Name = categoryNames.Dequeue(),
                    ParentCategory = pCategory
                };

                pCategory = category;
            }

            return category;
        }

        public class Offer
    {
        [JsonProperty("@type")]
        public string? Type { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Sku { get; set; }
        public string? Image { get; set; }
    }

    public class Offers
    {
        [JsonProperty("@type")]
        public string? Type { get; set; }
        public string? PriceCurrency { get; set; }
        public decimal Price { get; set; }
        public int ItemOffered { get; set; }
        public string? Availability { get; set; }
        public string? Url { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        [JsonProperty("offers")]
        public List<Offer>? OfferList { get; set; }
        public DateTime PriceValidUntil { get; set; }
    }

    public class Brand
    {
        [JsonProperty("@type")]
        public string? Type { get; set; }
        public string? Name { get; set; }
    }

    public class Review
    {
        [JsonProperty("@type")]
        public string? Type { get; set; }
        public string? Author { get; set; }
    }

    public class AggregateRating
    {
        [JsonProperty("@type")]
        public string? Type { get; set; }
        public int BestRating { get; set; }
        public int WorstRating { get; set; }
        public string? RatingValue { get; set; }
        public int ReviewCount { get; set; }
    }

    public class ProductParseModel
    {
        [JsonProperty("@context")]
        public string? Context { get; set; }

        [JsonProperty("@type")]
        public string? Type { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Sku { get; set; }

        public string? Url { get; set; }

        public string? Image { get; set; }

        public Offers? Offers { get; set; }

        public string? Gtin8 { get; set; }

        public Brand? Brand { get; set; }

        public List<Review>? Review { get; set; }

        public AggregateRating? AggregateRating { get; set; }
    }
}
}
