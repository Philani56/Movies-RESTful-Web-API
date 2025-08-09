using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult Get()
        {
            return Ok("This is accessible by Manager and Admin users");
        }

        [HttpGet("user-only")]
        [Authorize(Roles = "User")]
        public IActionResult GetUserOnly()
        {
            return Ok("This is accessible only by User role");
        }
    }
}
