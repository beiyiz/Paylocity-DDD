using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Paylocity.Deduction.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using Paylocity.Deduction.Infrastructure.Data.Config;

namespace Paylocity.Deduction.Infrastructure.Data.Config
{
    public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.ToTable("Dependent").HasKey(x => x.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);

            builder.Property(c => c.LastName)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);

            
            builder.Property(c => c.EmployeeId).IsRequired();

            builder.OwnsOne(p => p.DependentType, p =>
            {
                p.Property(pp => pp.Name).HasColumnName("DependentType_Name").HasMaxLength(ColumnConstants.DEFAULT_TYPE_LENGTH);
                p.Property(pp => pp.Description).HasColumnName("DependentType_Description").HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            });

            
        }
    }
}

