using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(100)]
        [MinLength(8)]
        [NotMapped]
        public string Password { get; set; }
        //[JsonIgnore]
        public bool IsAdmin { get; set; }
        public bool Active { get; set; } = true;
    }
}
