using System.Linq;
using AutoMapper;
using donet_rpg.Dtos.Character;
using donet_rpg.Dtos.Skill;
using donet_rpg.Dtos.Weapon;
using donet_rpg.Models;
using dotnet_rpg.Models;

namespace donet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>()
            .ForMember(dto => dto.Skills , c =>c.MapFrom(c=>c.CharacterSkills.Select(cs =>cs.Skill)));
            CreateMap<AddCharacterDto , Character>();
            CreateMap<Weapon , GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}