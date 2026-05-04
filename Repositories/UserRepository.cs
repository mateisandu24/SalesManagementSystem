using Dapper;
using SalesManagementSystem.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SalesManagementSystem.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        ///hash algorithm (SHA256)
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public User Authenticate(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                return connection.Query<User>(sql, new { Username = username, PasswordHash = hashedPassword }).FirstOrDefault();
            }
        }
    }
}