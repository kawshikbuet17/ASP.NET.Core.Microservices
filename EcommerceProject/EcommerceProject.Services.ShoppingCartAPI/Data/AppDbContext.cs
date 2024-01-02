using EcommerceProject.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}


// package manager console: add-migration addProductTable
// package manager console: update-database 