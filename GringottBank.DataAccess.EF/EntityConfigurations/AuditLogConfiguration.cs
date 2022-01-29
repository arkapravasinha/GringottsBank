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
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs")
                .HasKey(aul=>aul.Id);

            builder.Property(aul => aul.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
        }
    }
}
