using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLoan.Application.DTOs
{
    public class LoanInsertDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }


        [JsonIgnore]
        public DateTime LoanDate { get; set;}

        [JsonIgnore]
        public bool Delivered { get; set; }


    }
}
