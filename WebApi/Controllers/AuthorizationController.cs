using System;
using System.Threading.Tasks;
using Application.Authorization;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController:Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            if (!ModelState.IsValid) return BadRequest("Not correct data");
            try
            {
                var result = await _authorizationService.Login(data);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(403);
            }
        }
        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        { 
            await _authorizationService.Logout();
            return Ok();
        }
    }
}