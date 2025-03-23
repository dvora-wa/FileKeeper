//using BCrypt.Net-Next;
using BCrypt.Net;
using FileKeeper_server_.net.Core.Entities;
using FileKeeper_server_.net.Core.Interfaces.Repositories;
using FileKeeper_server_.net.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace FileKeeper_server_.net.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            if (await _userRepository.UserExistsAsync(email))
                return false;

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _userRepository.AddUserAsync(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
