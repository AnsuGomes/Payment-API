using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment_API.Models;

namespace Payment_API.Context
{
    public class ContextOrganizer
    {
        public ContextOrganizer(DbContextOptions<ContextOrganizer> options) : base (options)
        {

        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderSale> OrderSales { get; set; }
    }
}