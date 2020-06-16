using System.Threading.Tasks;
using donet_rpg.Models;

namespace donet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password );
        Task<bool> UserExists(string username);
    }
}