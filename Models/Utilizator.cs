using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public class Utilizator
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ParolaHash { get; set; }
        public string Rol { get; set; }
    }
}
