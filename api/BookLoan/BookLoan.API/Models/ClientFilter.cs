using System.ComponentModel.DataAnnotations;

namespace BookLoan.API.Models
{
    public class ClientFilter
    {
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must have 11 digits.")]
        public string Cpf { get; set; }

        [StringLength(250, ErrorMessage = "Name must have at most 250 characters.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "City must have at most 50 characters.")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "Neighborhood must have at most 50 characters.")]
        public string Neighborhood { get; set; }

        [StringLength(11, ErrorMessage = "Cell phone number must have at most 11 characters.")]
        public string PhoneNumber { get; set; }

        [StringLength(10, ErrorMessage = "Landline phone number must have at most 10 characters.")]
        public string FixPhoneNumber { get; set; }

        [Required(ErrorMessage = "Page number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page size is required.")]
        [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
        public int PageSize { get; set; }
    }
}