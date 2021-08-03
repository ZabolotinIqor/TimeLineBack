using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.ApplicationUser
{
    public class UserResolverService: IUserResolverService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;
        public UserResolverService(IHttpContextAccessor accessor,
            UserManager<Domain.Entities.ApplicationUser> userManager) {
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<Domain.Entities.ApplicationUser> GetUser() {
            var username = _accessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(username);
            return  user;
        }
    }
}