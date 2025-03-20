using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileKeeper_server_.net.Core.Entities;

namespace FileKeeper_server_.net.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
    }
}
