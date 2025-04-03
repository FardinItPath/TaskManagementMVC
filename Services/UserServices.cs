using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Models;
using TaskManagementMVC.Repository;

namespace TaskManagementMVC.Services
{
    public class UserServices : IUserServices
    {
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

        private readonly IUserRepository _userRepository;

        // Inject IUserRepository via constructor
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(UserModel user)
        {
            if (await IsUsernameExists(user))
            {
                return false; // Username already exists
            }

            return await _userRepository.RegisterUser(user);
        }


        public Task<bool> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameExists(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
