using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(User.FindFirst(ClaimTypes.Name)?.Value);
        }
        [HttpPost]
        public IActionResult LoginUser(string username)
        {
            var keyByte = System.Text.Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnmmnbvcxz");
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256);

            var security = new JwtSecurityToken(
                issuer:"Project1",
                audience:"room1",
                new Claim[]
                {
                    new Claim(ClaimTypes.Name , username)
                },
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: securityKey
            );

            var token = new JwtSecurityTokenHandler().WriteToken(security);

            return Ok( token );
        }
    }
}
