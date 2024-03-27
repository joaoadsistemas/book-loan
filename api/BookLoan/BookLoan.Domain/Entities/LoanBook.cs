using BookLoan.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Domain.Entities
{
    public class LoanBook
    {
        public int Id { get; private set; }
        public int LoanId { get; private set; }
        public Loan Loan { get; set; }
        public int BookId { get; private set; }
        public Book Book { get; set; }

        public LoanBook()
        {
            
        }

        public LoanBook(int id, int idLoan, int idBook)
        {
            DomainExceptionValidation.When(id < 0, "The book id should be more than 0.");
            Id = id;
            ValidateDomain(idLoan, idBook);
        }

        public LoanBook(int idLoan, int idBook)
        {
            ValidateDomain(idLoan, idBook);
        }

        public void Update(int idLoan, int idBook)
        {
            ValidateDomain(idLoan, idBook);
        }

        private void ValidateDomain(int idLoan, int idBook)
        {
            DomainExceptionValidation.When(idLoan <= 0, "The loan id is not valid.");
            DomainExceptionValidation.When(idBook < 0, "The book id is not valid.");
            LoanId = idLoan;
            BookId = idBook;
        }
    }
}
