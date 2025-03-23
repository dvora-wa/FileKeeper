using FileKeeper_server_.net.Core.Entities;
using System.Threading.Tasks;

namespace FileKeeper_server_.net.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> RegisterUserAsync(string email, string password);
    }
}
