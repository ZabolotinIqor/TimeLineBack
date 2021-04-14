using Domain.Common;
using Domain.Entities;

namespace Application.Token
{
    public class TokenService: ITokenService
    {
        public LoginResponseDto Execute(Domain.Entities.ApplicationUser user, RefreshToken refreshToken = null)
        {
            throw new System.NotImplementedException();
        }
    }
}