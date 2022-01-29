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
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany<Transaction>(acct => acct.Transactions)
                .WithOne(tran => tran.Account)
                .HasForeignKey(tran => tran.AccountID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Account")
                .HasKey(accnt => accnt.AccountID);

            builder.Property(accnt => accnt.AccountID)
                .UseIdentityColumn(seed: 10000, increment: 1)
                .ValueGeneratedOnAdd();

            builder.Property(accnt => accnt.AccountNickName)
                .HasDefaultValue(string.Empty)
                .HasMaxLength(50);

            builder.Property(accnt=>accnt.Nominee)
                .HasDefaultValue(string.Empty)
                .HasMaxLength(50);

            builder.Property(accnt => accnt.Balance)
                .HasDefaultValue(00.00);

            builder.Property(accnt => accnt.AccountType)
                .HasDefaultValue(AccountType.Savings)
                .IsRequired();

        }
    }
}
