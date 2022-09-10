using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using RecipeHub.ClassLib.Database.EfStructures;

namespace RecipeHub.ClassLib.Database.Infrastructure
{
    public class AppDbContextModule : Module
    {
        private readonly string _connectionString;

        public AppDbContextModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>()
                .WithParameter("options", AppDbContextFactory.GetOptions(_connectionString))
                .InstancePerLifetimeScope();
        }
    }
}
