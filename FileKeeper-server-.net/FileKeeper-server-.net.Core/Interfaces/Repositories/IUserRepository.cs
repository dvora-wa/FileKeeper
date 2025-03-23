using FileKeeper_server_.net.Core.Entities;
using System.Threading.Tasks;

namespace FileKeeper_server_.net.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
