using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public class Tranzactie
    {
        public Guid Id { get; set; }
        public Guid ProdusId { get; set; }
        public Guid ClientId { get; set; }
        public Guid UtilizatorId { get; set; } 
        public int Cantitate { get; set; }
        public DateTime DataTranzactie { get; set; }
    }
}
