﻿using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public string ThumbnailUrl { get; set; } = default!;
        public List<Variant> Variants { get; set; } = new();

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
