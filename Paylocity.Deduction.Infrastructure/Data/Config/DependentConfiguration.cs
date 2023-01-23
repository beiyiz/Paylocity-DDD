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

namespace Paylocity.Deduction.Infrastructure.Data.Config
{
    public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.ToTable("Dependent").HasKey(x => x.Id);

            builder.Property(c => c.Dependent_FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);

            builder.Property(c => c.Dependent_LastName)
                .IsRequired()
                .HasColumnName("LastName")
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);

            
            builder.Property(c => c.EmployeeId).IsRequired();
            builder.Property(c=>c.DependentTypeId).IsRequired();

            builder.HasOne(c => c.DependentType);

        }
    }
}