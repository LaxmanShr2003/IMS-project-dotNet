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
    internal class StockInfo_Configuration:IEntityTypeConfiguration<StockInfo>
    {
        public void Configure(EntityTypeBuilder<StockInfo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();



            builder.HasOne(e => e.ProductRateInfo)
           .WithMany(e => e.StockInfos)
           .HasForeignKey(e => e.ProductRateInfoId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CategoryInfo)
         .WithMany(e => e.StockInfos)
         .HasForeignKey(e => e.CategoryInfoId)
         .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StoreInfo)
         .WithMany(e => e.StockInfos)
         .HasForeignKey(e => e.StoreInfoId)
         .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Quantity)
          .HasMaxLength(200)
          .IsUnicode(true);

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
