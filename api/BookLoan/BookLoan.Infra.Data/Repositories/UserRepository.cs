using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly SystemDbContext _dbContext;

        public UserRepository(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            List<User> users = await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();
            return users;
        }

        public async Task<User> FindById(int id)
        {
            User user = await _dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == id)
                            ?? throw new ArgumentException($"User with id: {id} does not exists");
            return user;
        }

        public async Task<User> Insert(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {

            User user = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == id)
                        ?? throw new ArgumentException($"User with id: {id} does not exists");
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
