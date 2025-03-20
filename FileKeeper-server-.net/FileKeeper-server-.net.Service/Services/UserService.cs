using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileKeeper_server_.net.Core.Entities;
using FileKeeper_server_.net.Core.Interfaces;

namespace FileKeeper_server_.net.Service.Services
{
    public class UserService
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

        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            // כאן היינו מוסיפים Hashing לסיסמה (למשל עם BCrypt)
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _userRepository.AddUserAsync(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
