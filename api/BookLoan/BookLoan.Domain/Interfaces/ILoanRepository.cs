
namespace Loan.Domain.Interfaces
{
    public interface ILoanRepository
    {

        Task<IEnumerable<BookLoan.Domain.Entities.Loan>> FindAll();
        Task<BookLoan.Domain.Entities.Loan> FindById(int id);
        Task<BookLoan.Domain.Entities.Loan> Insert(BookLoan.Domain.Entities.Loan loan);
        Task<BookLoan.Domain.Entities.Loan> Update(BookLoan.Domain.Entities.Loan loan);
        Task<BookLoan.Domain.Entities.Loan> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}
