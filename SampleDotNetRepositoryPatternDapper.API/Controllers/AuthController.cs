using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleDotNetRepositoryPatternDapper.App;
using SampleDotNetRepositoryPatternDapper.Core;

namespace SampleDotNetRepositoryPatternDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JWTService _jWTService;
        public AuthController(UserService userService, JWTService jWTService)
        {
            _userService = userService;
            _jWTService = jWTService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> CreateToken(UserLogin userLogin)
        {
            try
            {
                User checkUser = await _userService.GetLoginUser(userLogin);
                if (checkUser != null) // Simple demo check
                {
                    var token = _jWTService.CreateToken(checkUser.UserName);
                    return Ok(new { token });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return BadRequest();
        }
    }
}
