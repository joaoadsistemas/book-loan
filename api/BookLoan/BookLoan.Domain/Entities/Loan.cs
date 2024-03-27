using BookLoan.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace BookLoan.Domain.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DeliveryDate { get; set;}
        public bool Delivered { get; set; }

        public Loan()
        {

        }

        public Loan(int id, int clientId, int bookId, DateTime loanDate, DateTime deliveryDate, bool delivered)
        {
            Id = id;
            ClientId = clientId;
            BookId = bookId;
            LoanDate = loanDate;
            DeliveryDate = deliveryDate;
            Delivered = delivered;
            ValidateDomain(loanDate, deliveryDate);
        }

        public Loan(int clientId, int bookId, DateTime loanDate, DateTime deliveryDate, bool delivered)
        {
            ClientId = clientId;
            BookId = bookId;
            LoanDate = loanDate;
            DeliveryDate = deliveryDate;
            Delivered = delivered;
            ValidateDomain(loanDate, deliveryDate);
        }

        public void Update(DateTime loanDate, DateTime deliveryDate)
        {
            ValidateDomain(loanDate, deliveryDate);
        }

        public void ValidateDomain(DateTime loanDate, DateTime deliveryDate)
        {
            DomainExceptionValidation.When(loanDate >= deliveryDate, "Delivery date must be after loan date");

            LoanDate = loanDate;
            DeliveryDate = deliveryDate;
        }

    }
}
