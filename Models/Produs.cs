using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public class Produs
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public string ImagineUrl { get; set; }
        public decimal Pret { get; set; }
        public int Stoc { get; set; }
        public decimal TVA { get; set; }
        public Guid? CategorieId { get; set; } 
        public Guid? BrandId { get; set; }

    }
}
