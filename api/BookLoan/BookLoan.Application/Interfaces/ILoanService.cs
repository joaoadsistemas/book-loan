using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;
using BookLoan.Domain.Pagination;

namespace BookLoan.Application.Interfaces
{
    public interface ILoanService
    {

        Task<PagedList<LoanDTO>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<LoanDTO>> FindByFilter(string cpf, string name,
            DateTime? loanDateInitial, DateTime? loanDateFinal, DateTime? deliverDateInitial,
            DateTime? deliverDateFinal, bool? delivered, bool? notDelivered, int pageNumber, int pageSize);
        Task<LoanDTO> FindById(int id);
        Task<LoanDTO> Insert(LoanInsertDTO loanInsertDto);
        Task<LoanDTO> Update(LoanDTO loanInsertDto);
        Task<bool> VerifyAvailable(int id); 
        Task<LoanDTO> Delete(int id);

    }
}
