using Microsoft.EntityFrameworkCore;
using ProjectProduct.Models;

namespace ProjectProduct.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)

        { 

        }
            
         public DbSet<Product> Products { get; set; }

        
    }
    }

