using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LogiCast.Infrastructure.Data;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath =  @"C:\Users\herem\backend\LogiCast\LogiCast.API";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder.UseNpgsql(connectionString);
            
            return new AppDbContext(builder.Options);
        }
    }