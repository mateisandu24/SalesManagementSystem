using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }
    }
}
