using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Entities;

namespace BookLoan.Domain.Interfaces
{
    public interface ILoanBookRepository
    {
        Task<IEnumerable<LoanBook>> FindAllByLoan(int id);
        Task<LoanBook> FindById(int id);
        Task<LoanBook> Insert(LoanBook loanbooks);
        Task<IEnumerable<LoanBook>> InsertManyAsync(IEnumerable<LoanBook> loanbooks);
        Task<IEnumerable<LoanBook>> UpdateAllAsync(List<LoanBook> loanbooks);
        Task<LoanBook> Delete(int id);
        
        
    }
}
