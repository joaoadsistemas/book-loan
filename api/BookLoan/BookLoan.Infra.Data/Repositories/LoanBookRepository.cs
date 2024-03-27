using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.Infra.Data.Repositories
{
    public class LoanBookRepository : ILoanBookRepository
    {
        private readonly SystemDbContext _dbContext;

        public LoanBookRepository(SystemDbContext context)
        {
            _dbContext = context;
        }

        public async Task<LoanBook> Update(LoanBook loanBook)
        {
            _dbContext.LoanBooks.Update(loanBook);
            await _dbContext.SaveChangesAsync();
            return loanBook;
        }

        public async Task<LoanBook> Delete(int id)
        {
            var loanBook = await _dbContext.LoanBooks.FindAsync(id);
            if (loanBook != null)
            {
                _dbContext.LoanBooks.Remove(loanBook);
                await _dbContext.SaveChangesAsync();
                return loanBook;
            }

            return null;
        }


        public async Task<LoanBook> Insert(LoanBook loanBook)
        {
            _dbContext.LoanBooks.Add(loanBook);
            await _dbContext.SaveChangesAsync();
            return loanBook;
        }

        public async Task<IEnumerable<LoanBook>> InsertManyAsync(IEnumerable<LoanBook> loanBooks)
        {
            var idsBooksValidos = await _dbContext.Books
            .Where(l => loanBooks.Select(le => le.BookId).Contains(l.Id))
            .Select(l => l.Id)
            .ToListAsync();

            var loanBooksValidos = loanBooks
                .Where(le => idsBooksValidos.Contains(le.BookId))
                .ToList();

            _dbContext.LoanBooks.AddRange(loanBooksValidos);
            await _dbContext.SaveChangesAsync();
            return loanBooks;
        }

        public async Task<LoanBook> FindById(int id)
        {
            return await _dbContext.LoanBooks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LoanBook>> FindAllByLoan(int id)
        {
            return await _dbContext.LoanBooks.Where(x => x.LoanId == id).Include(x => x.Book).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<LoanBook>> UpdateAllAsync(List<LoanBook> loanBooks)
        {
            if (loanBooks.Count == 0)
            {
                return new List<LoanBook>();
            }

            var idsBooksValidos = await _dbContext.Books
            .Where(l => loanBooks.Select(le => le.BookId).Contains(l.Id))
            .Select(l => l.Id)
            .ToListAsync();

            var loanBooksValidos = loanBooks
                .Where(le => idsBooksValidos.Contains(le.BookId))
                .ToList();

            var loanBooksByLoan = _dbContext.LoanBooks.Where(x => x.LoanId == loanBooks[0].LoanId);
            _dbContext.LoanBooks.RemoveRange(loanBooksByLoan);

            _dbContext.LoanBooks.AddRange(loanBooksValidos);
            await _dbContext.SaveChangesAsync();
            return loanBooks;
        }
    }
}
