using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using EventApi.Data;

namespace EventApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EventDbContext>
    {
        public EventDbContext CreateDbContext(string[] args)
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            
            var projectFolder = Path.GetFullPath(Path.Combine(assemblyFolder, "..", "..", ".."));
            
            var jsonPath = Path.Combine(projectFolder, "appsettings.json");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("appsettings.json not found at " + jsonPath);

            var config = new ConfigurationBuilder()
                .SetBasePath(projectFolder)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionString 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<EventDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EventDbContext(optionsBuilder.Options);
        }
    }
}