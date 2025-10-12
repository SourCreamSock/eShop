using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Persistence.Catalog
{
    public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        /// <summary>
        /// Create DbContext using local db
        /// </summary>                
        public CatalogContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Catalogdb;Integrated Security=True;Connect Timeout=30;" +
                "Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
