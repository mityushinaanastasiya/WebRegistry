using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebRegistry.Models;
namespace WebRegistry
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ElectronicRegistryDataBaseContext>
    {
        public ElectronicRegistryDataBaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ElectronicRegistryDataBaseContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ElectronicRegistryDataBaseContext(builder.Options);
        }
    }
}
