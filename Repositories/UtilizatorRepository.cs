using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SalesManagementSystem.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SalesManagementSystem.Repositories
{
    public class UtilizatorRepository
    {
        private readonly string _connectionString;
        public UtilizatorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// hash check
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

        public Utilizator CheckPassword(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Utilizatori WHERE Username = @Username AND ParolaHash = @ParolaHash";
                return connection.QueryFirstOrDefault<Utilizator>(sql, new { Username = username, ParolaHash = hashedPassword });
            }
        }

    }
}
