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
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(50);
            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Neighborhood).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Number).IsRequired().HasMaxLength(20);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(c => c.FixPhoneNumber).IsRequired().HasMaxLength(10);

        }
    }
}
