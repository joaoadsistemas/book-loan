using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [NotMapped] // nao faz parte do mapeamento do automapper
        [Required]
        [MaxLength(100)]
        [MinLength(8)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
