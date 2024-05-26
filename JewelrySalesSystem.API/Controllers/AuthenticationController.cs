using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            try
            {
                var result = await _userService.LoginAsync(email, password);

                if (result)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return BadRequest(new
            {
                ErrorMessage = "Wrong UserName or Password"
            });
        }
    }
}
