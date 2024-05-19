using IMS.models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.infrastructure.Entity_Configuration
{
    public class ProductInvoiceDetailInfo_Configuration : IEntityTypeConfiguration<ProductInvoiceDetailInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInvoiceDetailInfo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Rate)
               .HasColumnType("float");
            builder.Property(e => e.Quantity)
           .HasColumnType("float");
            builder.Property(e => e.Amount)
              .HasColumnType("float");

            builder.HasOne(e => e.ProductInvoiceInfo)
.WithMany(e => e.ProductInvoiceDetailInfos)
.HasForeignKey(e => e.ProductInvoiceInfoId);

            builder.HasOne(e => e.ProductRateInfo)
.WithMany(e => e.ProductInvoiceDetailInfos)
.HasForeignKey(e => e.ProductRateInfoId);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);

        }

  
    }
}
