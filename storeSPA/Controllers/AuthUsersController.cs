#nullable disable
namespace storeSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly IUserAuthetication _Service;
        public AuthUsersController(IUserAuthetication service)
        {
            _Service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new LoginResult
                {
                    Errors = new List<string> { "Enter The Required Fields"},
                    Result = false,
                });

            try
            {
                LoginResult result = await _Service.Authenticate(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new LoginResult
                {
                    Errors = new List<string> { "Un Expected Error" },
                    Result = false,
                });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new { message = "Fill The Required Fields" });
            try
            {
                LoginResult result = await _Service.Register(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new LoginResult
                {
                    Errors = new List<string> { "Un Expected Error" },
                    Result = false,
                });
            }
        }
    }
}
