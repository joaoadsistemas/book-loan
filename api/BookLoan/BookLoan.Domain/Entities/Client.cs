using System.ComponentModel.DataAnnotations;
using BookLoan.Domain.Validations;

namespace BookLoan.Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string PhoneNumber { get; set; }
        public string FixPhoneNumber { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Loan> Loan { get; set; }

        public Client()
        {
            
        }

        public Client(int id, string cpf, string name, string address, string city, string neighborhood,
            string number, string phoneNumber, string fixPhoneNumber)
        {
            Id = id;
            ValidateDomain(cpf, name, address, city, neighborhood, number, phoneNumber, fixPhoneNumber);
        }

        public Client(string cpf, string name, string address, string city, string neighborhood, 
            string number, string phoneNumber, string fixPhoneNumber)
        {
            ValidateDomain(cpf, name, address, city, neighborhood, number, phoneNumber, fixPhoneNumber);
        }

        public void Update(string cpf, string name, string address, string city, string neighborhood,
            string number, string phoneNumber, string fixPhoneNumber)
        {
            ValidateDomain(cpf, name, address, city, neighborhood, number, phoneNumber, fixPhoneNumber);
        }

        public void Delete()
        {
            Deleted = true;
        }

        public void ValidateDomain(string cpf, string name, string address, string city, string neighborhood,
            string number, string phoneNumber, string fixPhoneNumber)
        {

            DomainExceptionValidation.When(cpf.Length != 11, "Cpf should have 11 characters");
            DomainExceptionValidation.When(name.Length > 200, "The name must have a maximum of 200 characters");
            DomainExceptionValidation.When(address.Length > 50, "The address must have a maximum of 50 characters");
            DomainExceptionValidation.When(city.Length > 50, "The city must have a maximum of 50 characters");
            DomainExceptionValidation.When(neighborhood.Length > 100, "The neighborhood must have a maximum of 100 characters");
            DomainExceptionValidation.When(number.Length > 20, "The number must have a maximum of 20 characters");
            DomainExceptionValidation.When(phoneNumber.Length > 11, "The phone number must have a maximum of 11 characters");
            DomainExceptionValidation.When(fixPhoneNumber.Length > 10, "The fix phone number must have a maximum of 10 characters");





            Cpf = cpf;
            Name = name;
            Address = address;
            City = city;
            Neighborhood = neighborhood;
            Number = number;
            PhoneNumber = phoneNumber;
            FixPhoneNumber = fixPhoneNumber;
        }
    }
}
