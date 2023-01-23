using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.Deduction.Core.Aggregates;

namespace Paylocity.Deduction.Infrastructure.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee").HasKey(x => x.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.LastName)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);

            builder.Property(e => e.Deductables).HasColumnType("decimal(18,2)");

            
        }
    }
}
