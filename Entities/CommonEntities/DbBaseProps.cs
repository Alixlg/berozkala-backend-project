using berozkala_backend.Interfaces;

namespace berozkala_backend.Entities.CommonEntities
{
    public class DbBaseProps : IGuid
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
    }
}