using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StoreServiceApp
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreCS")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }
}