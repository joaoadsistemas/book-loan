using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using BookLoan.Domain.Pagination;
using BookLoan.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly SystemDbContext _dbContext;

        public ClientRepository(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<Client>> FindAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Clients.Where(c => !c.Deleted)
                .AsNoTracking()
                .AsQueryable();

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);

        }

        public async Task<PagedList<Client>> FindByFilter(string cpf, string name, string city, string neighbourhood, string phoneNumber, string fixPhoneNumber,
            int pageNumber, int pageSize)
        {
            var query = _dbContext.Clients.Where(x => !x.Deleted).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(x => x.Cpf.Equals(cpf));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Equals(name.ToLower())
                                         || x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(x => x.City.ToLower().Equals(city.ToLower())
                                         || x.City.ToLower().Contains(city.ToLower()));
            }

            if (!string.IsNullOrEmpty(neighbourhood))
            {
                query = query.Where(x => x.Neighborhood.ToLower().Equals(neighbourhood.ToLower())
                                         || x.Neighborhood.ToLower().Contains(neighbourhood.ToLower()));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Equals(phoneNumber));
            }

            if (!string.IsNullOrEmpty(fixPhoneNumber))
            {
                query = query.Where(x => x.FixPhoneNumber.Equals(fixPhoneNumber));
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<Client>> FindByFilter(string term, int pageNumber, int pageSize)
        {

            var query = _dbContext.Clients.Where(x => !x.Deleted).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();

                query = query.Where(x =>
                    x.Name.ToLower().Contains(term) ||
                    x.Cpf.Contains(term) ||
                    x.City.ToLower().Contains(term) ||
                    x.Neighborhood.ToLower().Contains(term) ||
                    x.PhoneNumber.Contains(term) ||
                    x.FixPhoneNumber.Contains(term)
                );
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);

        }

        public async Task<Client> FindById(int id)
        {
            Client client = await _dbContext.Clients
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted)
                            ?? throw new ArgumentException($"Client with id: {id} does not exists");
            return client;
        }

        public async Task<Client> Insert(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Delete(int id)
        {

            Client client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id)
                            ?? throw new ArgumentException($"Client with id: {id} does not exists");
            client.Delete();
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
            return client;


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
