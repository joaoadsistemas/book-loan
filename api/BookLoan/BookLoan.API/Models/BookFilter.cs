using System.ComponentModel.DataAnnotations;

namespace BookLoan.API.Models
{
    public class BookFilter
    {
        [MaxLength(50, ErrorMessage = "The book's name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "The author's name cannot exceed 200 characters.")]
        public string Author { get; set; }

        [MaxLength(50, ErrorMessage = "The publisher's name cannot exceed 50 characters.")]
        public string Publisher { get; set; }

        public DateTime? YearOfPublication { get; set; }

        [MaxLength(50, ErrorMessage = "The edition field of the book cannot exceed 50 characters.")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "Page number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "Page size is required.")]
        [Range(1, 50, ErrorMessage = "Page size must be between 1 and 50.")]
        public int PageSize { get; set; }
    }
}