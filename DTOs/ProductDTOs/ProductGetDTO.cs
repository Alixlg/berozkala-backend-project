using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.ProductEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.ProductDTOs
{
    public class ProductGetDTO
    {
        public Guid Id { get; set; }
        public DateTime DateToAdd { get; set; }
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public required List<ProductSubCategory> Category { get; set; }
        public required decimal Price { get; set; }
        public required int MaxCount { get; set; }
        public Score ScoreRank { get; set; }
        public decimal DiscountPercent { get; set; }
        public string? PreviewImageUrl { get; set; }
        public ProductImage? ImagesUrls { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public List<ProductGarranty>? Garrantys { get; set; }
        public List<ProductAttribute>? Attributes { get; set; }
    }
}