using LogiCast.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


namespace LogiCast.Infrastructure.Data;

public class AppDbContext : DbContext
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        
        public DbSet<Category> Category { get; set; } 
        public DbSet<Inventory> Inventory { get; set; } 
        public DbSet<Item> Item { get; set; } 
        public DbSet<Warehouse> Warehouse { get; set; }
}