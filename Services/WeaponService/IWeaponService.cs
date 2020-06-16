using System.Threading.Tasks;
using donet_rpg.Dtos.Character;
using donet_rpg.Dtos.Weapon;
using donet_rpg.Models;



namespace donet_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}