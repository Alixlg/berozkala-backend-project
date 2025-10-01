namespace berozkala_backend.DTOs.MemberDTOs
{
    public class MemberSingUpDto
    {
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
    }
}