using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Product;

namespace berozkala_backend.DTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string? DateToAdd { get; set; }
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public required string Category { get; set; }
        public required double Price { get; set; }
        public required int MaxCount { get; set; }
        public int ScoreRank { get; set; }
        public double DiscountPercent { get; set; }
        public string? PreviewImageUrl { get; set; }
        public string[]? ImagesUrl { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public List<ProductGarranty>? Garrantys { get; set; }
        public List<ProductAttribute>? Attributes { get; set; }
    }
}