using BookLoan.API.Context;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using BookLoan.Domain.Pagination;
using BookLoan.Infra.Data.Helpers;
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

        public async Task<PagedList<User>> FindAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Users
                .AsNoTracking()
                .AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<User>> FindAllByFilterAsync(string name, string email, bool? isAdmin, bool? isNotAdmin, bool? active, bool? inactive, int pageNumber, int pageSize)
        {
            var query = _dbContext.Users.OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Equals(name.ToLower())
                                         || x.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.ToLower().Equals(email.ToLower())
                                         || x.Email.ToLower().Contains(email.ToLower()));
            }

            if (isAdmin.HasValue && isAdmin == true)
            {
                query = query.Where(x => x.IsAdmin == true);
            }
            if (isNotAdmin.HasValue && isNotAdmin == true)
            {
                query = query.Where(x => x.IsAdmin == false);
            }
            if (active.HasValue && active == true)
            {
                query = query.Where(x => x.Active == true);
            }
            if (inactive.HasValue && inactive == true)
            {
                query = query.Where(x => x.Active == false);
            }

            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
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
            if (user.PasswordSalt == null || user.PasswordHash == null)
            {
                var passwordCript = await _dbContext.Users.Where(x => x.Id == user.Id).Select(x => new { x.PasswordHash, x.PasswordSalt }).FirstOrDefaultAsync();
                user.ChangePassword(passwordCript.PasswordHash, passwordCript.PasswordSalt);
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExistsUserRegistered()
        {

            return await _dbContext.Users.AnyAsync();

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
