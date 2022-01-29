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
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction")
                .HasKey(tran=>tran.TransactionID);

            builder.Property(tran => tran.TransactionID)
                .HasDefaultValueSql("NEWID()");

            builder.Property(tran => tran.TransactionType)
                .IsRequired();

            builder.Property(tran => tran.Amount)
                .IsRequired();

            builder.Property(tran => tran.Time)
                .HasDefaultValue(DateTime.Now);

            builder.Property(tran => tran.Reference)
                .HasMaxLength(100);
        }
    }
}
