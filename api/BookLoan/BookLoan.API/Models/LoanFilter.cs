using System.ComponentModel.DataAnnotations;

namespace BookLoan.API.Models
{
    public class LoanFilter
    {
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must have 11 digits.")]
        public string Cpf { get; set; }

        [StringLength(250, ErrorMessage = "Name must have at most 250 characters.")]
        public string Name { get; set; }

        public DateTime? LoanDateInitial { get; set; }
        public DateTime? LoanDateFinal { get; set; }

        public DateTime? DeliverDateInitial { get; set; }
        public DateTime? DeliverDateFinal { get; set; }

        public bool? Delivered { get; set; }
        public bool? NotDelivered { get; set; }

        [Required(ErrorMessage = "Page number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page size is required.")]
        [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
        public int PageSize { get; set; }
    }
}