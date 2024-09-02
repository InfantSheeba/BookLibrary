using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BookLibraryManagementSystem.Models;
using System.Data.SqlClient;

namespace BookLibraryManagementSystem.Data
{
   
        public interface IUserRepository
        {
            Task<Users> GetUserByIdAsync(int id);
            Task<IEnumerable<Users>> GetAllUsersAsync();
            Task AddUserAsync(Users user);
            //Task UpdateUserAsync(Users user);
            //Task DeleteUserAsync(int id);
        }
 }
    /// <summary>
    /// Repository for get sql connection 
    /// </summary>
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateUserAsync(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO BookUsers (FirstName, LastName, Email, Password, ConfirmPassword, PhoneNumber, DateOfBirth) VALUES (@FirstName, @LastName, @Email, @Password, @ConfirmPassword, @PhoneNumber, @DateOfBirth)";
                await connection.ExecuteAsync(sql, user);
            }
        }
    }

