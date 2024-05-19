using IMS.infrastructure.Entity_Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.infrastructure
{
    public class ImsDbContext: DbContext
    {
        public ImsDbContext(DbContextOptions<ImsDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.ApplyConfiguration(new StoreConfiguration());
          
           
            Builder.ApplyConfiguration(new Category_Configuration());
            Builder.ApplyConfiguration(new Customer_Configuration());
            Builder.ApplyConfiguration(new UnitInfo_Configuration());
            Builder.ApplyConfiguration(new ProductInfo_Configuration());
            Builder.ApplyConfiguration(new RackInfo_Configuration());
            Builder.ApplyConfiguration(new SupplierInfo_Configuration());
            Builder.ApplyConfiguration(new ProductRateInfo_Configuration());
            Builder.ApplyConfiguration(new StockInfo_Configuration());
            Builder.ApplyConfiguration(new TransationInfo_Configuration());
            Builder.ApplyConfiguration(new ProductInvoiceInfo_Configuration());
            Builder.ApplyConfiguration(new ProductInvoiceDetailInfo_Configuration());
        }
    }



}
