
using BookLoan.Domain.Pagination;

namespace Loan.Domain.Interfaces
{
    public interface ILoanRepository
    {

        Task<PagedList<BookLoan.Domain.Entities.Loan>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<BookLoan.Domain.Entities.Loan>> FindByFilter(string cpf, string name,
            DateTime? loanDateInitial, DateTime? loanDateFinal, DateTime? deliverDateInitial,
            DateTime? deliverDateFinal, bool? delivered, bool? notDelivered, int pageNumber, int pageSize);
        Task<BookLoan.Domain.Entities.Loan> FindById(int id);
        Task<BookLoan.Domain.Entities.Loan> Insert(BookLoan.Domain.Entities.Loan loan);
        Task<BookLoan.Domain.Entities.Loan> Update(BookLoan.Domain.Entities.Loan loan);
        Task<bool> VerifyAvailable(int id);
        Task<BookLoan.Domain.Entities.Loan> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}
