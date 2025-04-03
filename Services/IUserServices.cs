using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Models;
using TaskManagementMVC.Repository;

namespace TaskManagementMVC.Services
{
    public interface IUserServices
    {
    Task<bool> RegisterUser(UserModel user);  // Register a new user
    Task<UserModel> AuthenticateUser(string username, string password); 
    Task<bool> IsUsernameExists(UserModel user);
    Task<bool> GetUserByUsername(string username);
    }
}
