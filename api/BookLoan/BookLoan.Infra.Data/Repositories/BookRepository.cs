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
    public class BookRepository : IBookRepository
    {

        private readonly SystemDbContext _dbContext;

        public BookRepository(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Book>> FindAll()
        {
            IEnumerable<Book> books = await _dbContext.Books.AsNoTracking().ToListAsync();
            return books;
        }

        public async Task<Book> FindById(int id)
        {
            Book book = await _dbContext.Books
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == id)
                            ?? throw new ArgumentException($"Book with id: {id} does not exists");
            return book;
        }

        public async Task<Book> Insert(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Delete(int id)
        {

            Book book = await _dbContext.Books.FirstOrDefaultAsync(c => c.Id == id)
                            ?? throw new ArgumentException($"Book with id: {id} does not exists");
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
