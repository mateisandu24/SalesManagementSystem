using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
