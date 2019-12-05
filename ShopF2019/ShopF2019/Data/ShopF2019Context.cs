using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopF2019.Models;

namespace ShopF2019.Models
{
    public class ShopF2019Context : DbContext
    {
        public ShopF2019Context (DbContextOptions<ShopF2019Context> options)
            : base(options)
        {
        }

        public DbSet<ShopF2019.Models.Shopper> Shopper { get; set; }

        public DbSet<ShopF2019.Models.Cart> Cart { get; set; }

        public DbSet<ShopF2019.Models.Order> Order { get; set; }

        public DbSet<ShopF2019.Models.Product> Product { get; set; }
    }
}
