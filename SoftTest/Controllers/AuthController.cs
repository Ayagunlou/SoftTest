using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoftTest.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SoftTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            CreatePasswordHash(request.Password,  out byte[] passwordhash, out byte[] passwordsalt);
            user.UserName = request.Username;
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordsalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            if(user.UserName!= request.Username)
            {
                return BadRequest("User not found.");
            }
            if(!VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmc = new HMACSHA512(passwordsalt))
            {
                var computehash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computehash.SequenceEqual(passwordhash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
