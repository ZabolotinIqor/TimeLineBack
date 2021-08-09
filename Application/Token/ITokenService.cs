using Domain.Common;
using Domain.Entities;

namespace Application.Token
{
    public interface ITokenService
    {
        LoginResponseDto Execute(Domain.Entities.ApplicationUser user, RefreshToken refreshToken = null);
    }
}