using System.ComponentModel.DataAnnotations;

namespace BookLoan.API.Models
{
    public class TermSearch
    {
        [Required(ErrorMessage = "Search term is required")]
        [MaxLength(300, ErrorMessage = "The term cannot exceed 300 characters.")]
        public string Term { get; set; }

        [Required(ErrorMessage = "Page number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page size is required")]
        [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
        public int PageSize { get; set; }

    }
}
