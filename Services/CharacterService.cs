using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using donet_rpg.Data;
using donet_rpg.Dtos.Character;
using donet_rpg.Models;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace donet_rpg.Services
{

    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;

            _mapper = mapper;
            _context = context;
        }

        private int GetUserId()=> int.Parse(_httpContextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.Characters
            .Include(c=>c.CharacterSkills).ThenInclude(CookieSecurePolicy=>CookieSecurePolicy.Skill)
            .Include(c=>c.Weapon)
            .FirstOrDefaultAsync(c => c.Id == id && c.User.Id== GetUserId());
            
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character ch = _mapper.Map<Character>(newCharacter);
            ch.User = await _context.Users.FirstOrDefaultAsync(u => u.Id==GetUserId());
            await _context.AddAsync(ch);
            await _context.SaveChangesAsync();


            serviceResponse.Data = (_context.Characters.Where(c=>c.User.Id==GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {

            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.Include(c=>c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if(character.User.Id == GetUserId())
                {
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;

                _context.Characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(_context.Characters.ToListAsync());
            }
            else
            {
                serviceResponse.Success=false;
                serviceResponse.Message="Character Not Found";
            }
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                Character character = await _context.Characters.FirstAsync(c => c.Id == id && c.User.Id==GetUserId());
                if(character!= null)
                {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            
                serviceResponse.Data = (_context.Characters.Where(c=>c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();

                }
                else{
                    serviceResponse.Message="Character not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}