using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface ILoanService
    {

        Task<IEnumerable<LoanDTO>> FindAll();
        Task<LoanDTO> FindById(int id);
        Task<LoanDTO> Insert(LoanInsertDTO loanInsertDto);
        Task<LoanDTO> Update(LoanDTO loanInsertDto);
        Task<LoanDTO> Delete(int id);

    }
}
