using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class LoanBookDTO
    {

        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int LoanId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }
        public BookDTO Book { get; set; }

    }
}
