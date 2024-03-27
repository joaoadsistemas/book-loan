using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
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

        public async Task<IEnumerable<Client>> FindAll()
        {
            List<Client> clients = await _dbContext.Clients.Where(c => !c.Deleted)
                .AsNoTracking()
                .ToListAsync();
            return clients;
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
