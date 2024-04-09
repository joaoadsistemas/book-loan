using System.ComponentModel.DataAnnotations;

namespace BookLoan.API.Models
{
    public class UserFilter
    {
        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public bool? IsAdmin { get; set; }
        public bool? IsNotAdmin { get; set; }
        public bool? Active { get; set; }
        public bool? Inactive { get; set; }

        [Required(ErrorMessage = "Page number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page size is required.")]
        [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
        public int PageSize { get; set; }
    }
}