using System.Threading.Tasks;
using activity_data.Repository;
using activity_model;
using activity_data.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace activity_api.Controllers
{
    [ApiController,Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public UserController(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
           var username = user.UserName.ToLower();
            if (await _repository.UserExist(username))
                return BadRequest($"{user.UserName} already exists!");
            var userToCreate = new User{
                UserName = username
            };
            var createUser = await _repository.Register(userToCreate,user.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserDto userDto)
        {
            var user = await _repository.Login(userDto.UserName.ToLower(),userDto.Password);
            if (user is null)
              return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}