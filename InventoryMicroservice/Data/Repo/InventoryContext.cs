using InventoryMicroservice.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMicroservice.Data.Repo
{
    public class InventoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
