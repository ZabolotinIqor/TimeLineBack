using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [Route("api/[controller]")]
    public class ApiBaseController: ControllerBase
    {
        
    }
}