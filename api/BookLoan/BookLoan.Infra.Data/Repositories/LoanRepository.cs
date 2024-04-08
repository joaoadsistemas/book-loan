using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.API.Context;
using BookLoan.Domain.Pagination;
using BookLoan.Infra.Data.Helpers;
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

        public async Task<PagedList<BookLoan.Domain.Entities.Loan>> FindAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Loans
                .Include(l => l.Client)
                .AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<BookLoan.Domain.Entities.Loan>> FindByFilter(string cpf, string name, DateTime? loanDateInitial, DateTime? loanDateFinal,
            DateTime? deliverDateInitial, DateTime? deliverDateFinal, bool? delivered, bool? notDelivered, int pageNumber,
            int pageSize)
        {
            var query = _dbContext.Loans.Include(x => x.Client).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(x => x.Client.Cpf.Equals(cpf));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Client.Name.ToLower().Equals(name.ToLower())
                                         || x.Client.Name.ToLower().Contains(name.ToLower()));
            }

            if (loanDateInitial.HasValue)
            {
                query = query.Where(x => loanDateInitial.Value <= x.LoanDate);
            }

            if (loanDateFinal.HasValue)
            {
                query = query.Where(x => x.LoanDate <= loanDateFinal.Value);
            }

            if (deliverDateInitial.HasValue)
            {
                query = query.Where(x => deliverDateInitial.Value <= x.DeliveryDate);
            }

            if (deliverDateFinal.HasValue)
            {
                query = query.Where(x => x.DeliveryDate <= deliverDateFinal.Value);
            }

            if (delivered.HasValue && delivered == true)
            {
                query = query.Where(x => x.Delivered == true);
            }

            if (notDelivered.HasValue && notDelivered == true)
            {
                query = query.Where(x => x.Delivered == false);
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<BookLoan.Domain.Entities.Loan> FindById(int id)
        {
            BookLoan.Domain.Entities.Loan loan = await _dbContext.Loans
                .Include(l => l.Client)
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

        public async Task<bool> VerifyAvailable(int[] idBooks)
        {

            var verifyLoan = await _dbContext.Loans
                .Where(x => x.LoanBooks.Any(lb => idBooks.Contains(lb.BookId)) && !x.Delivered)
                .AnyAsync();
            return !verifyLoan;

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
