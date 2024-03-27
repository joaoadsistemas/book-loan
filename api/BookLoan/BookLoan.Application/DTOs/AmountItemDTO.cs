using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class AmountItemDTO
    {
        public int BookAmount { get; set; }
        public int ClientAmount { get; set; }
        public int LoanAmount { get; set; }
    }
}
