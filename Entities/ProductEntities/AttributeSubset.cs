using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class AttributeSubset : DbBaseProps
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
        public int AttributeId { get; set; }
        public ProductAttribute? Attribute { get; set; }
    }
}