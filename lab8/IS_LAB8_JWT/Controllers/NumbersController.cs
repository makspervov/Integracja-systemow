using Microsoft.AspNetCore.Mvc;
using IS_LAB8_JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IS_LAB8_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NumbersController : ControllerBase
    {
        static bool czyPierwsza(int a)
        {
            if (a % 2 == 0)
                return (a == 2);
            for (int i = 3; i <= Math.Sqrt(a); i += 2)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "number", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("random_prime_number")]
        public int randomNumber(AuthenticationRequest request)
        {
            Random rnd = new Random();
            int number = rnd.Next(2, 14);

            while (!czyPierwsza(number))
            {
                number = rnd.Next(2, 14);
            }

            return number;
        }
    }
}
