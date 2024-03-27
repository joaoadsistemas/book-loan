using BookLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLoan.API.Context
{
    public class SystemDbContext : DbContext
    {

        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Domain.Entities.Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemDbContext).Assembly);

        }
    }
}
