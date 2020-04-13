using System.Threading.Tasks;
using activity_data.Repository;
using activity_model;
using activity_model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace activity_api.Controllers
{
    [ApiController,Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public UserController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
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
    }
}