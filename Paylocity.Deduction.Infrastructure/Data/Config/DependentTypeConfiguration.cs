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
using Paylocity.Deduction.Core.ValueObjects;

namespace Paylocity.Deduction.Infrastructure.Data.Config
{
    public class DependentTypeConfiguration : IEntityTypeConfiguration<DependentType>
    {
        public void Configure(EntityTypeBuilder<DependentType> builder)
        {
            builder.ToTable("DependentType").HasKey(x => new { x.Name, x.Description});

            builder.Property(c => c.Name)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_TYPE_LENGTH);
            builder.Property(c => c.Description)
                .IsRequired()
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);           
            
        }
    }
}