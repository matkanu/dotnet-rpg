using System.Threading.Tasks;
using donet_rpg.Data;
using donet_rpg.Dtos.User;
using donet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace donet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Authentication : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public Authentication(IAuthRepository authRepository)
        {
           _authRepository = authRepository;

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> response = await _authRepository.Register(
                new User { UserName = request.UserName}, request.Password
            );

           if(!response.Success)
           { 
               return BadRequest(response);
           }
           return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRegisterDto request)
        {
            ServiceResponse<string> response = await _authRepository.Login(
                 request.UserName, request.Password
            );

           if(!response.Success)
           { 
               return BadRequest(response);
           }
           return Ok(response);
        }
        
    }
}