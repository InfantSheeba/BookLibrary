
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BookLibraryManagementSystem.Data;
using BookLibraryManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookLibraryManagementSystem.Data
{
    public interface IUserRepository
    {
        Task<BookUsers> GetUserByEmailAsync(string email);
        Task<int> RegisterUserAsync(BookUsers user);
    }
}



namespace BookLibraryManagementSystem.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<BookUsers> GetUserByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<BookUsers>(
                    "SELECT * FROM BookCustomers WHERE Email = @Email", new {Email = email});
            }
        }

        public async Task<int> RegisterUserAsync(BookUsers user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO BookCustomers (FirstName, LastName, Email, Password,ConfirmPassword, PhoneNumber, DateOfBirth) " +
                          "VALUES (@FirstName, @LastName, @Email, @Password,@ConfirmPassword, @PhoneNumber, @DateOfBirth);";
                return await connection.ExecuteAsync(sql, user);
            }
        }
    }
}
