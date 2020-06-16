using System.Threading.Tasks;
using donet_rpg.Dtos.Character;
using donet_rpg.Dtos.CharacterSkill;
using donet_rpg.Models;

namespace donet_rpg.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
         Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkilldto newCharacterSkill);
    }
}