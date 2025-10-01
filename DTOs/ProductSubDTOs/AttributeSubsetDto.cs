using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class AttributeSubsetDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public AttributeSubset ToDatabaseEntitie()
        {
            return new AttributeSubset()
            {
                Name = Name,
                Value = Value
            };
        }
    }
}