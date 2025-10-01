using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class ProductGarrantyDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public long GarrantyCode { get; set; }
        public ProductGarranty ToDatabaseEntitie()
        {
            return new ProductGarranty()
            {
                Name = Name,
                GarrantyCode = GarrantyCode
            };
        }
    }
}