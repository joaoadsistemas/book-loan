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
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Author).HasMaxLength(200).IsRequired();
            builder.Property(b => b.Publisher).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Edition).HasMaxLength(50).IsRequired();


        }
    }
}
