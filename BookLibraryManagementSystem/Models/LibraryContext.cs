using Microsoft.Data.SqlClient;
using System.Data;

namespace BookLibraryManagementSystem.Data
{
    /// <summary>
    /// LibraryContext manages database connections.
    /// </summary>
    public class LibraryContext
    {
        private readonly string _connectionString;

        public LibraryContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
