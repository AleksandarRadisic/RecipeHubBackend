using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Recipe>()
                .HasMany(r => r.RecipeIngredients)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<Recipe>()
                .HasMany(r => r.Pictures)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<Recipe>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Recipe)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder
                .Entity<Article>()
                .HasMany(r => r.Pictures)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
