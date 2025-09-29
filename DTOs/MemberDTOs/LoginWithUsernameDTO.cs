namespace berozkala_backend.DTOs.MemberDTOs
{
    public class LoginWithUsernameDTO
    {
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
    }
}