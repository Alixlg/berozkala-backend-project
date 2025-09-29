namespace berozkala_backend.Entities.CommonEntities
{
    public class DbBaseProps
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
    }
}