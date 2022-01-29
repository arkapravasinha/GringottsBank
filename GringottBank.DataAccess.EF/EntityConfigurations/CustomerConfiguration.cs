using GringottBank.DataAccess.EF.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.EF.EntityConfigurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer")
                .HasKey(entity=>entity.CustomerId);

            builder.Property(entity => entity.CustomerId)
                .UseIdentityColumn(seed: 100, increment: 1)
                .ValueGeneratedOnAdd();

            builder.Property(entity => entity.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(entity => entity.Email)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(entity => entity.Email)
              .HasMaxLength(256)
              .IsRequired();

            builder.Property(entity => entity.Mobile)
              .HasMaxLength(10)
              .IsRequired();

            builder.HasMany<Account>(cust => cust.Accounts)
                .WithOne(accnt => accnt.Customer)
                .HasForeignKey(accnt => accnt.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
