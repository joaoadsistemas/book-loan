using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Validations;

namespace BookLoan.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public User()
        {
            
        }

        public User(string name, string email)
        {
            ValidateDomain(name, email);
        }

        public User(int id, string name, string email)
        {
            Id = id;
            ValidateDomain(name, email);

        }

        public void SetAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public void ChangePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private void ValidateDomain(string name, string email)
        {
            DomainExceptionValidation.When(name == null, "Name is required");
            DomainExceptionValidation.When(email == null, "Name is required");
            DomainExceptionValidation.When(name.Length > 250, "Name does not exceed 250 characters");
            DomainExceptionValidation.When(email.Length > 200, "Name does not exceed 200 characters");

            Name = name;
            Email = email;
            IsAdmin = false;
        }

    }
}
