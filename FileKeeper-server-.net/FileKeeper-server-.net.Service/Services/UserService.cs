using FileKeeper_server_.net.Core.Entities;
using FileKeeper_server_.net.Core.Interfaces.Repositories;
using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Core.Models;

namespace FileKeeper_server_.net.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(RegisterUserModel model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new Exception("משתמש עם אימייל זה כבר קיים במערכת");
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> Login(LoginUserModel model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                throw new Exception("שם משתמש או סיסמה שגויים");
            }
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("משתמש לא נמצא");
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task UpdateUser(int id, UpdateUserModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("משתמש לא נמצא");
            }

            if (model.FirstName != null) user.FirstName = model.FirstName;
            if (model.LastName != null) user.LastName = model.LastName;
            if (model.Email != null) user.Email = model.Email;
            if (model.Password != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("משתמש לא נמצא");
            }
            await _userRepository.DeleteAsync(user);
        }
    }
}