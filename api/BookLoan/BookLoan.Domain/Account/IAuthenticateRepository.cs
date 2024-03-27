using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Entities;

namespace BookLoan.Domain.Account
{
    public interface IAuthenticateRepository
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> VerifyUserExistsByEmail(string email);
        public string GenerateToken(int id, string email);
        public Task<User> GetUserByEmail(string email);
    }
}
