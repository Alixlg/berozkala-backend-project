using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs
{
    public class ProductPreviewDTO
    {
        public required string Id { get; set; }
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public required List<string> Category { get; set; }
        public required double Price { get; set; }
        public required int MaxCount { get; set; }
        public required Score ScoreRank { get; set; }
        public required double DiscountPercent { get; set; }
        public string? PreviewImageUrl { get; set; }
    }
}