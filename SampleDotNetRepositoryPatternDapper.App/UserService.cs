using SampleDotNetRepositoryPatternDapper.Core;
using System.Collections;

namespace SampleDotNetRepositoryPatternDapper.App
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> GetUserAsync(int id) => _userRepository.GetByIdAsync(id);

        public void AddUser(User user)
        {
            _userRepository.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateAsync(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<User>> GetAllUsers() => _userRepository.GetAllAsync();

        public Task<User> GetLoginUser(UserLogin user)
        {
            return _userRepository.LoginUser(user);
        }
    }
}
