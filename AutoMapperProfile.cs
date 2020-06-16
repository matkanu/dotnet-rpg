using AutoMapper;
using donet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace donet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto , Character>();
        }
    }
}