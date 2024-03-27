using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Author { get; set; }

        [Required]
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        public DateTime YearOfPublication { get; set; }

        [Required]
        [MaxLength(50)]
        public string Edition { get; set; }

    }
}
