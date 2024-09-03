using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Models;
using Dapper;


namespace BookLibraryManagementSystem.Data
{
    public interface IUserRepository
    {
        Task CreateUserAsync(BookUsers user);
        Task<BookUsers> GetUserByUsernameAsync(string username);
    }
}

namespace BookLibraryManagementSystem.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateUserAsync(BookUsers user)
        {
            var sql = @"INSERT INTO BookUsers (FirstName, LastName, Email, Password, ConfirmPassword, PhoneNumber, DateOfBirth)
                        VALUES (@FirstName, @LastName, @Email, @Password, @ConfirmPassword, @PhoneNumber, @DateOfBirth)";
            await _dbConnection.ExecuteAsync(sql, user);
        }

        public async Task<BookUsers> GetUserByUsernameAsync(string username)
        {
            var sql = "SELECT * FROM BookUsers WHERE Email = @Username";
            return await _dbConnection.QueryFirstOrDefaultAsync<BookUsers>(sql, new { Username = username });
        }
    }
}
