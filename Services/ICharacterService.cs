using System.Collections.Generic;
using System.Threading.Tasks;
using donet_rpg.Dtos.Character;
using donet_rpg.Models;
using dotnet_rpg.Models;

namespace donet_rpg.Services
{
    public interface ICharacterService
    {

        Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto  newCharacter);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id);
    }
}