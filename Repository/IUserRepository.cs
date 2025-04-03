using TaskManagementMVC.Models;

namespace TaskManagementMVC.Repository
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(UserModel user);
        Task<UserModel> AuthenticateUser(string username, string password);
        Task<bool> IsUsernameExists(string username);
        Task<UserModel> GetUserByUsername(string username);
    }
}
