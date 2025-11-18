using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TalentoLocal.Models
{
    public class DbDevopsContextFactory : IDesignTimeDbContextFactory<DbDevopsContext>
    {
        public DbDevopsContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbDevopsContext>();

            optionsBuilder.UseSqlServer(
                config.GetConnectionString("AzureSqlConnection")
            );

            return new DbDevopsContext(optionsBuilder.Options);
        }
    }
}
