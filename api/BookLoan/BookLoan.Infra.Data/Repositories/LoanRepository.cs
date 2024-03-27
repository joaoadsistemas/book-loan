using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.API.Context;
using Loan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infra.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly SystemDbContext _dbContext;

        public LoanRepository(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookLoan.Domain.Entities.Loan>> FindAll()
        {
            IEnumerable<BookLoan.Domain.Entities.Loan> loans = await _dbContext.Loans
                .Include(l => l.Client)
                .Include(l => l.Book)
                .ToListAsync();
            return loans;
        }

        public async Task<BookLoan.Domain.Entities.Loan> FindById(int id)
        {
            BookLoan.Domain.Entities.Loan loan = await _dbContext.Loans
                .Include(l => l.Client)
                .Include(l => l.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new ArgumentException($"Loan with id: {id} does not exists");
            return loan;
        }

        public async Task<BookLoan.Domain.Entities.Loan> Insert(BookLoan.Domain.Entities.Loan loan)
        {
            _dbContext.Loans.Add(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<BookLoan.Domain.Entities.Loan> Update(BookLoan.Domain.Entities.Loan loan)
        {
            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<BookLoan.Domain.Entities.Loan> Delete(int id)
        {

            BookLoan.Domain.Entities.Loan loan = await _dbContext.Loans.FirstOrDefaultAsync(c => c.Id == id)
                                                 ?? throw new ArgumentException($"Loan with id: {id} does not exists");
            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
            return loan;


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
