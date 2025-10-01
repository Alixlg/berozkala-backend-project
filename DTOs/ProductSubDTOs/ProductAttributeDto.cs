using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class ProductAttributeDto
    {
        public Guid? Id { get; set; }
        public required string TitleName { get; set; }
        public required List<AttributeSubsetDto> Subsets { get; set; }
        public ProductAttribute ToDatabaseEntitie()
        {
            return new ProductAttribute()
            {
                TitleName = TitleName,
                Subsets = Subsets.Select(s => s.ToDatabaseEntitie()).ToList()
            };
        }
    }
}