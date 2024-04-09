using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using BookLoan.Domain.Pagination;
using BookLoan.Infra.Data.Helpers;
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


        public async Task<PagedList<Book>> FindAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Books.Where(b => !b.Removed).AsNoTracking().AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Book> FindById(int id)
        {
            return await _dbContext.Books.Where(x => !x.Removed).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<PagedList<Book>> FindByFilter(string name, string author, string publisher, DateTime? yearOfPublication, string edition, int pageNumber, int pageSize)
        {
            var query = _dbContext.Books.Where(x => !x.Removed).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Equals(name.ToLower())
                                         || x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(x => x.Author.ToLower().Equals(author.ToLower())
                                         || x.Author.ToLower().Contains(author.ToLower()));
            }

            if (!string.IsNullOrEmpty(publisher))
            {
                query = query.Where(x => x.Publisher.ToLower().Equals(publisher.ToLower())
                                         || x.Publisher.ToLower().Contains(publisher.ToLower()));
            }

            if (yearOfPublication.HasValue)
            {
                query = query.Where(x => x.YearOfPublication == yearOfPublication.Value);
            }

            if (!string.IsNullOrEmpty(edition))
            {
                query = query.Where(x => x.Edition.ToLower().Equals(edition.ToLower())
                                         || x.Edition.ToLower().Contains(edition.ToLower()));
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }


        public async Task<PagedList<Book>> FindByFilter(string term, int pageNumber, int pageSize)
        {
            var query = _dbContext.Books.Where(x => !x.Removed).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();

                query = query.Where(x =>
                    x.Name.ToLower().Contains(term) ||
                    x.Author.ToLower().Contains(term) ||
                    x.Publisher.ToLower().Contains(term) ||
                    x.Edition.ToLower().Contains(term)
                );
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
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
            book.Remove();
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
