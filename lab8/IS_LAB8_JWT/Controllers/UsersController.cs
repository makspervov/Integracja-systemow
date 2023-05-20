using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IS_LAB8_JWT.Model;
using IS_LAB8_JWT.Services.Users;
using IS_LAB8_JWT.Entities;

namespace IS_LAB8_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            var response = userService.Authenticate(request);
            if (response == null)
                return BadRequest(new{message = "Username or password is incorrect" });
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all_authenticate")]
        public IEnumerable<User> allAuthenticateUsers(AuthenticationRequest request)
        {
            return userService.GetUsers();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("authenticate_users_count")]
        public string NumberOfAuthenticateUsers(AuthenticationRequest request)
        {
            int users_count = userService.GetUsers().Count();
            return $"Number of users in the system: {users_count}";
        }
    }
}
