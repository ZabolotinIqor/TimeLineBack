using System.Threading.Tasks;
using Application.ApplicationUser;
using Domain.Common.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicationUserController:ApiBaseController
    {
        private readonly IApplicationUserService _userService;
        public ApplicationUserController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("/getUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _userService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/updateUser")]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            var result = await _userService.Update(user);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}