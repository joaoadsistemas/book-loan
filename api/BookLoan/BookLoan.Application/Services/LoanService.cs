using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;

using BookLoan.Domain.Entities;
using BookLoan.Domain.Pagination;
using Loan.Domain.Interfaces;

namespace LoanLoan.Application.Services
{
    public class LoanService : ILoanService
    {

        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<LoanDTO>> FindAll(int pageNumber, int pageSize)
        {
            var loans = await _loanRepository.FindAll(pageNumber, pageSize);
            IEnumerable<LoanDTO> loanDtos = _mapper.Map<IEnumerable<LoanDTO>>(loans);
            return new PagedList<LoanDTO>(loanDtos, pageNumber, pageSize, loans.TotalCount);

        }

        public async Task<PagedList<LoanDTO>> FindByFilter(string cpf, string name, DateTime? loanDateInitial, DateTime? loanDateFinal,
            DateTime? deliverDateInitial, DateTime? deliverDateFinal, bool? delivered, bool? notDelivered, int pageNumber,
            int pageSize)
        {
            var loans = await _loanRepository.FindByFilter(cpf, name, loanDateInitial, loanDateFinal, deliverDateInitial, deliverDateFinal, delivered, notDelivered, pageNumber, pageSize);
            var loansDTO = _mapper.Map<IEnumerable<LoanDTO>>(loans);
            return new PagedList<LoanDTO>(loansDTO, pageNumber, pageSize, loans.TotalCount);
        }

        public async Task<LoanDTO> FindById(int id)
        {
            BookLoan.Domain.Entities.Loan loan = await _loanRepository.FindById(id);
            LoanDTO loanDto = _mapper.Map<LoanDTO>(loan);
            return loanDto;
        }

        public async Task<LoanDTO> Insert(LoanInsertDTO loanInsertDTO)
        {

            BookLoan.Domain.Entities.Loan loan = _mapper.Map<BookLoan.Domain.Entities.Loan>(loanInsertDTO);
            BookLoan.Domain.Entities.Loan loanInserted = await _loanRepository.Insert(loan);
            return _mapper.Map<LoanDTO>(loanInserted);

        }

        public async Task<LoanDTO> Update(LoanDTO loanDTO)
        {
            BookLoan.Domain.Entities.Loan loan = _mapper.Map<BookLoan.Domain.Entities.Loan>(loanDTO);
            BookLoan.Domain.Entities.Loan loanChanged = await _loanRepository.Update(loan);
            return _mapper.Map<LoanDTO>(loanChanged);
        }

        public async Task<bool> VerifyAvailable(int id)
        {

            return await _loanRepository.VerifyAvailable(id);
           
        }

        public async Task<LoanDTO> Delete(int id)
        {
            BookLoan.Domain.Entities.Loan loanDeleted = await _loanRepository.Delete(id);
            return _mapper.Map<LoanDTO>(loanDeleted);
        }
    }
}
