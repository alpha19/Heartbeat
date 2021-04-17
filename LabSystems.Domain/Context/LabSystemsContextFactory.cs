using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace LabSystems.Domain.Context
{
    public class LabSystemsContextFactory : IDesignTimeDbContextFactory<LabSystemsContext>
    {
        public LabSystemsContext CreateDbContext(string[] argsb = null)
        {
            var options = new DbContextOptionsBuilder<LabSystemsContext>();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("datasettings.json").Build();

            var connection = config.GetSection("DefaultConnection");

            options.UseSqlServer(connection.Value);
            LabSystemsContext context = new LabSystemsContext(options.Options);

            return context;
        }
    }
}
