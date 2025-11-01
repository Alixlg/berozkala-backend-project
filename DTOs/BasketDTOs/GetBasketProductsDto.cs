using berozkala_backend.DTOs.ProductDTOs;
using berozkala_backend.DTOs.ProductSubDTOs;

namespace berozkala_backend.DTOs.BasketDTOs
{
    public class GetBasketProductDto
    {
        public required Guid Id { get; set; }
        public required ProductPreviewDto Product { get; set; }
        public int ProductCount { get; set; }
        public GarrantyDto? SelectedGarranty { get; set; }
    }
}