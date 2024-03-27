using System.ComponentModel.DataAnnotations;

namespace BookLoan.Application.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }


        [Required]
        [StringLength(11)]
        [MinLength(11)]
        public string Cpf { get; set; }


        [Required]
        [StringLength(200)]
        public string Name { get; set; }


        [Required]
        [StringLength(50)]
        public string Address { get; set; }


        [Required]
        [StringLength(50)]
        public string City { get; set; }


        [Required]
        [StringLength(100)]
        public string Neighborhood { get; set; }


        [Required]
        [StringLength(20)]
        public string Number { get; set; }


        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }


        [Required]
        [StringLength(10)]
        public string FixPhoneNumber { get; set; }
    }
}
