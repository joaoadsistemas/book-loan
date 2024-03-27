using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.API.Context;
using BookLoan.Domain.Interfaces;
using BookLoan.Domain.SystemModels;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.Infra.Data.Repositories
{
    public class SystemRepository : ISystemRepository
    {

        private readonly SystemDbContext _dbContext;

        public SystemRepository(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AmountItem> SelectItemAmount()
        {
            AmountItem itemAmout = new AmountItem();
            itemAmout.BookAmount = await _dbContext.Books.CountAsync();
            itemAmout.ClientAmount = await _dbContext.Clients.CountAsync();
            itemAmout.LoanAmount = await _dbContext.Loans.CountAsync();
            return itemAmout;
        }
    }
}
