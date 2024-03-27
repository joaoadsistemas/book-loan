using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;

using BookLoan.Domain.Entities;
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

        public async Task<IEnumerable<LoanDTO>> FindAll()
        {
            IEnumerable<BookLoan.Domain.Entities.Loan> loans = await _loanRepository.FindAll();
            IEnumerable<LoanDTO> loanDtos = _mapper.Map<IEnumerable<LoanDTO>>(loans);
            return loanDtos;

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

        public async Task<LoanDTO> Delete(int id)
        {
            BookLoan.Domain.Entities.Loan loanDeleted = await _loanRepository.Delete(id);
            return _mapper.Map<LoanDTO>(loanDeleted);
        }
    }
}
