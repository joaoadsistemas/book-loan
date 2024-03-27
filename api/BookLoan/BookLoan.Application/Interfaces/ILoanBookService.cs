using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface ILoanBookService
    {
        Task<IEnumerable<LoanBookDTO>> FindAllByLoan(int id);
        Task<LoanBookDTO> FindById(int id);
        Task<LoanBookDTO> Insert(LoanBookDTO loanbooks);
        Task<IEnumerable<LoanBookDTO>> InsertManyAsync(IEnumerable<LoanBookDTO> loanbooks);
        Task<IEnumerable<LoanBookDTO>> UpdateAllAsync(List<LoanBookDTO> loanbooks);
        Task<LoanBookDTO> Delete(int id);
    }
}
