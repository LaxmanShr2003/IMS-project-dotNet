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
    public  class Customer_Configuration : IEntityTypeConfiguration<CustomerInfo>
    {
        public void Configure(EntityTypeBuilder<CustomerInfo> builder)
        {


            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(true);

           
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Address)
               .HasMaxLength(250)
               .IsUnicode(true);

            builder.Property(e => e.PanNo)
               .HasMaxLength(50)
               .IsUnicode(true);

            builder.HasOne(e => e.StoreInfo)
            .WithMany(e => e.CustomerInfos)
            .HasForeignKey(e => e.StoreInfoId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreatedDate)
              .IsRequired()
               .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);

          

        }
    }
}
