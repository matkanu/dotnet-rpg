using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using donet_rpg.Dtos.Character;
using donet_rpg.Models;
using donet_rpg.Services;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace donet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }
        
        [HttpGet("getall")]
        public async Task<IActionResult> Get()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c=> c.Type ==ClaimTypes.NameIdentifier).Value);
            return  Ok( await _characterService.GetAllcharacters(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        { 
            return  Ok(await _characterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto  newcharacter)
        {
           
            return  Ok(await _characterService.AddCharacter(newcharacter));
        }

       [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto  updateCharacter)
        {
           ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
           if(response.Data== null)
           {
               return NotFound(response);
           }

            return  Ok(await _characterService.UpdateCharacter(updateCharacter));
        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}