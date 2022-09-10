using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecipeHub.ClassLib.Database.EfStructures
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = SetupOptions(args[0]);
            return new AppDbContext(optionsBuilder.Options);
        }

        public static DbContextOptions<AppDbContext> GetOptions(string connectionString)
        {
            var optionsBuilder = SetupOptions(connectionString);
            return optionsBuilder.Options;
        }

        private static DbContextOptionsBuilder<AppDbContext> SetupOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder;
        }
    }
}
