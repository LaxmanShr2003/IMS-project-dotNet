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
    public class ProductInfo_Configuration : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {


            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
                .HasMaxLength(200)
                .IsUnicode(true);

       

            builder.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .IsUnicode(true);

            

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(300)
                .IsUnicode(true);

            builder.HasOne(e => e.CategoryInfo)
         .WithMany(e => e.ProductInfos)
         .HasForeignKey(e => e.CategoryInfoId);

            builder.HasOne(e => e.UnitInfo)
      .WithMany(e => e.ProductInfos)
      .HasForeignKey(e => e.UnitInfoId);

            builder.HasOne(e => e.StoreInfo)
      .WithMany(e => e.ProductInfos)
      .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.StockInfos)
           .WithOne(e => e.ProductInfo)
           .HasForeignKey(e => e.ProductInfoId);

            builder.HasMany(e => e.TransationInfos)
         .WithOne(e => e.ProductInfo)
         .HasForeignKey(e => e.ProductInfoId);

           

            builder.HasMany(e => e.ProductRateInfos)
         .WithOne(e => e.ProductInfo)
         .HasForeignKey(e => e.ProductInfoId);





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
