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
    public class TransationInfo_Configuration : IEntityTypeConfiguration<TransationInfo>
    {
        public void Configure(EntityTypeBuilder<TransationInfo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TransactionType)
              .HasMaxLength(200)
              .IsUnicode(true);



            builder.Property(e => e.Rate)
               .HasColumnType("float");


            builder.Property(e => e.Quantity)
              .HasMaxLength(200)
              .IsUnicode(true);

          

            builder.Property(e => e.Amount)
       .HasColumnType("float");

            builder.HasOne(e => e.CategoryInfo)
       .WithMany(e => e.TransationInfos)
       .HasForeignKey(e => e.CategoryInfoId)
        .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductInfo)
      .WithMany(e => e.TransationInfos)
      .HasForeignKey(e => e.ProductInfoId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UnitInfo)
     .WithMany(e => e.TransationInfos)
     .HasForeignKey(e => e.UnitInfoId)
      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StoreInfo)
     .WithMany(e => e.TransationInfos)
     .HasForeignKey(e => e.StoreInfoId)
      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductRateInfo)
     .WithMany(e => e.TransationInfos)
     .HasForeignKey(e => e.ProductRateInfoId)
      .OnDelete(DeleteBehavior.Restrict);

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
