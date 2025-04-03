using System.Data;
using Dapper;
using TaskManagementMVC.Models;

namespace TaskManagementMVC.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        //  Register User
        public async Task<bool> RegisterUser(UserModel user)
        {
            _db.Open();
            string query = @"INSERT INTO Users (Username, Password, GenderId, IsActive, CreatedBy, CreatedDT) 
                             VALUES (@Username, @Password, @GenderId, 1, @CreatedBy, GETDATE())";
            var result = await _db.ExecuteAsync(query, user);
            return result > 0;
        }

        //  Authenticate User (Login)
        public async Task<UserModel> AuthenticateUser(string username, string password)
        {
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1";
            return await _db.QueryFirstOrDefaultAsync<UserModel>(query, new { Username = username, Password = password });
        }

        //  Check  Username Exists
        public async Task<bool> IsUsernameExists(string username)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE username = @Username";
            int count = await _db.ExecuteScalarAsync<int>(query, new { Username = username });
            return count > 0;
        }

        //  Get User by Username
        public async Task<UserModel> GetUserByUsername(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = @Username";
            return await _db.QueryFirstOrDefaultAsync<UserModel>(query, new { Username = username });
        }
    }
}
