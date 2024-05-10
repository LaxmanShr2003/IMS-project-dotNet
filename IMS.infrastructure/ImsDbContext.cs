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
        }
    }

}
