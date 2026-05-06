using Dapper;
using SalesManagementSystem.Models;
using System;
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

        public bool Register(User user, Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        user.PasswordHash = HashPassword(user.PasswordHash); 

                        string customerSql = @"INSERT INTO Customers (FirstName, LastName, Email) 
                                       OUTPUT INSERTED.Id 
                                       VALUES (@FirstName, @LastName, @Email)";

                        var customerId = connection.QuerySingle<Guid>(customerSql, customer, transaction);

                        string userSql = @"INSERT INTO Users (Username, PasswordHash, Role) 
                                   VALUES (@Username, @PasswordHash, @Role)";

                        connection.Execute(userSql, new
                        {
                            Username = user.Username,
                            PasswordHash = user.PasswordHash,
                            Role = (int)Role.User
                        }, transaction);

                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();

                        return false;
                    }
                }
            }
        }
    }
}