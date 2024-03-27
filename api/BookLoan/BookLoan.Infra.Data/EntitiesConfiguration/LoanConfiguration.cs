using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLoan.Infra.Data.EntitiesConfiguration
{
    internal class LoanConfiguration : IEntityTypeConfiguration<Domain.Entities.Loan>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Loan> builder)
        {

            builder.HasKey(bl => bl.Id);
            builder.Property(bl => bl.ClientId).IsRequired();
            builder.Property(bl => bl.DeliveryDate).IsRequired();
            builder.Property(bl => bl.Delivered).IsRequired();
            builder.Property(bl => bl.LoanDate).IsRequired();


            builder.HasOne(bl => bl.Client)
                .WithMany(c => c.Loan)
                .HasForeignKey(bl => bl.ClientId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
