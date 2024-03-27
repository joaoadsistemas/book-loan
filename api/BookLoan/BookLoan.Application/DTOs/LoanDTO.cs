using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ClientDTO Client { get; set; }
        public int BookId { get; set; }
        public BookDTO Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool Delivered { get; set; }

    }
}
