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
    public class ProductRateInfo_Configuration : IEntityTypeConfiguration<ProductRateInfo>
    {
        public void Configure(EntityTypeBuilder<ProductRateInfo> builder)
        {


            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(e => e.StoreInfo)
           .WithMany(e => e.ProductRateInfos)
           .HasForeignKey(e => e.StoreInfoId);

            builder.HasOne(e => e.CategoryInfo)
        .WithMany(e => e.ProductRateInfos)
        .HasForeignKey(e => e.CategroyInfoId);

            builder.Property(e => e.CostPrice)
       .HasColumnType("float");

            builder.Property(e => e.SellingPrice)
          .HasColumnType("float");

            builder.Property(e => e.Quantity)
        .HasColumnType("float");

            builder.Property(e => e.SoldQuantity)
         .HasColumnType("float");
            builder.Property(e => e.RemainingQuantity)
           .HasColumnType("float");

            builder.Property(e => e.BatchNo)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.PurchaseDate)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.ExpiryDate)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.HasOne(e => e.SupplierInfo)
          .WithMany(e => e.ProductRateInfos)
          .HasForeignKey(e => e.SupplierInfoId);


            builder.HasOne(e => e.RackInfo)
          .WithMany(e => e.ProductRateInfos)
          .HasForeignKey(e => e.RackInfoId);


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
