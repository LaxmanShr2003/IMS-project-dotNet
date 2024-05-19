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
    public class ProductInvoiceInfo_Configuration : IEntityTypeConfiguration<ProductInvoiceInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInvoiceInfo> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();
            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(200)
                .IsUnicode(true);
            builder.Property(e => e.InvoiceNo)
          .HasMaxLength(200)
          .IsUnicode(true);
            builder.Property(e => e.NetAmount)
           .HasColumnType("float");
            builder.Property(e => e.DiscountAmount)
                 .HasColumnType("float");

            builder.Property(e => e.TotalAmount)
          .HasColumnType("float");
          
            builder.Property(e => e.Remarks)
          .HasMaxLength(200)
          .IsUnicode(true);
            builder.Property(e => e.BillStatus)
          .HasMaxLength(200)
          .IsUnicode(true);
            builder.Property(e => e.CancellationRemarks)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.HasOne(e => e.CustomerInfo)
            .WithMany(pt => pt.ProductInvoiceInfos)
            .HasForeignKey(e => e.CustomerInfoId);

            builder.HasOne(e => e.StoreInfo)
           .WithMany(e => e.ProductInvoiceInfos)
           .HasForeignKey(e => e.StoreinfoId);


            builder.Property(e => e.IsActive)
           .HasDefaultValue(true);

            builder.Property(e => e.CreatedDate)
                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy)
                .IsUnicode(true);
        }
    }
}
