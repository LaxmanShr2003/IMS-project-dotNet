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
    public class RackInfo_Configuration : IEntityTypeConfiguration<RackInfo>
    {
        public void Configure(EntityTypeBuilder<RackInfo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(e => e.StoreInfo)
           .WithMany(e => e.RackInfos)
           .HasForeignKey(e => e.StoreInfoId);

            builder.Property(e => e.RackName)
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
