using System.Threading.Tasks;
using Application.ApplicationUser;
using Application.Token;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace Application.Authorization
{
    public class AuthorizationService:IAuthorizationService
    {
        private readonly ITokenService tokenService;
        private readonly IApplicationUserService applicationUserService;
        private readonly SignInManager<Domain.Entities.ApplicationUser> signInManager;
        
        public AuthorizationService(ITokenService tokenService, IApplicationUserService applicationUserService)
        {
            this.tokenService = tokenService;
            this.applicationUserService = applicationUserService;
        }
        
        public async Task<LoginResponseDto> Login(LoginDto request)
        {
            var result = await this.signInManager.PasswordSignInAsync(request.login, request.password, false, false);
            if (result.Succeeded)
            {
                var user = await this.applicationUserService.Get(request);
                await signInManager.SignInAsync(user, false);
                return this.tokenService.Execute(user);
            }
            return null;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}