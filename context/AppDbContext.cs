using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using supermercadoAPI.Models;

namespace supermercadoAPI.context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<ItemDeposito> ItemDeposito { get; set; }
    }
    
}