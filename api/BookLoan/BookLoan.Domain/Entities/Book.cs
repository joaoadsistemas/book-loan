using BookLoan.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace BookLoan.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime YearOfPublication { get; set; }
        public string Edition { get; set; }

        public ICollection<Loan> Loan { get; set; }



        public Book()
        {

        }

        public Book(int id, string name, string author, string publisher, DateTime yearOfPublication, string edition)
        {
            Id = id;
            ValidateDomain(name, author, publisher, yearOfPublication, edition);
        }

        public Book(string name, string author, string publisher, DateTime yearOfPublication, string edition)
        {
            ValidateDomain(name, author, publisher, yearOfPublication, edition);
        }

        public void Update(string name, string author, string publisher, DateTime yearOfPublication, string edition)
        {
            ValidateDomain(name, author, publisher, yearOfPublication, edition);
        }

        public void ValidateDomain(string name, string author, string publisher, DateTime yearOfPublication, string edition)
        {
            DomainExceptionValidation.When(name.Length > 50, "The name must have a maximum of 50 characters");
            DomainExceptionValidation.When(author.Length > 200, "The author must have a maximum of 200 characters");
            DomainExceptionValidation.When(publisher.Length > 50, "The publisher must have a maximum of 50 characters");
            DomainExceptionValidation.When(edition.Length > 50, "The edition must have a maximum of 50 characters");

            Name = name;
            Author = author;
            Publisher = publisher;
            YearOfPublication = yearOfPublication;
            Edition = edition;
        }
    }
}
