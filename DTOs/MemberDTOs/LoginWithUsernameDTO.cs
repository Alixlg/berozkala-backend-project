namespace berozkala_backend.DTOs.MemberDTOs
{
    public class LoginWithUsernameDto
    {
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
    }
}