using Domain.Enums;

namespace Domain.Common
{
    public class LoginResponseDto
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public int expiresIn { get; set; }
        public string userId { get; set; }
        // public Role role { get; set; }
        // public string birthDate { get; set; }
    }
}