using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLoan.Infra.Data.EntitiesConfiguration
{
    internal class LoanBookConfiguration : IEntityTypeConfiguration<LoanBook>
    {

        public void Configure(EntityTypeBuilder<LoanBook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BookId).IsRequired();
            builder.Property(x => x.LoanId).IsRequired();
            builder.HasOne(x => x.Book).WithMany(x => x.LoanBooks)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Loan).WithMany(x => x.LoanBooks)
                .HasForeignKey(x => x.LoanId).OnDelete(DeleteBehavior.NoAction);
        }


    }
}
