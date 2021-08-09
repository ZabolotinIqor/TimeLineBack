using System;
using System.Threading.Tasks;
using Application.ApplicationUser;
using Application.Token;
using Domain.Common;
using Microsoft.AspNetCore.Identity;


namespace Application.Authorization
{
    public class AuthorizationService:IAuthorizationService
    {
        private readonly ITokenService _tokenService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly SignInManager<Domain.Entities.ApplicationUser> _signInManager;
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;
        
        public AuthorizationService(ITokenService tokenService, IApplicationUserService applicationUserService,
            SignInManager<Domain.Entities.ApplicationUser> signInManager,
            UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _applicationUserService = applicationUserService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public async Task<LoginResponseDto> Login(LoginDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (!(user is null))
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.password, false, false);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return _tokenService.Execute(user);
                }
            }
            else
            {
                throw new Exception("There is no such user by this email");
            }
            return null;
        }
        public async Task<LoginResponseDto> Register(RegistrationDto registrationDto)
        {
            var user = new Domain.Entities.ApplicationUser
            {
                UserName = registrationDto.Name,
                Email = registrationDto.Email,
                PhoneNumber = registrationDto.PhoneNumber
                
            };
            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return _tokenService.Execute(user);
            }
            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}