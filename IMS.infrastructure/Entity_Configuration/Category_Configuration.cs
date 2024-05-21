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
    public class Category_Configuration : IEntityTypeConfiguration<CategoryInfo>
    {
        public void Configure(EntityTypeBuilder<CategoryInfo> builder)
        {


            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();
            builder.Property(e => e.CategoryName)
                .HasMaxLength(200)
                .IsUnicode(true);
            builder.Property(e => e.CategoryDescription)
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.HasOne(e => e.StoreInfo)
            .WithMany(pt => pt.CategoryInfos)
            .HasForeignKey(e => e.StoreInfoId)
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
