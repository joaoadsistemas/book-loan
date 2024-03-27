using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class LoanUpdateDTO
    {

        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public bool Delivered { get; set; }

    }
}
