using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worldbuilder.Model;

namespace Worldbuilder.Models
{
    public class WorldbuilderContext : DbContext
    {
        public WorldbuilderContext (DbContextOptions<WorldbuilderContext> options)
            : base(options)
        {
        }
      

        public DbSet<Worldbuilder.Model.Brick> Brick { get; set; }
        public DbSet<Worldbuilder.Model.Category> Categories { get; set; }
        public DbSet<Worldbuilder.Model.BrickCategory> BrickCategories { get; set; }
        public DbSet<Worldbuilder.Model.BrickToBrick> BrickToBrick { get; set; }
        public DbSet<Worldbuilder.Model.CategoryType> CategoryTypes { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrickCategory>().HasKey(sc => new { sc.BrickId, sc.CategoryId });
            

            modelBuilder.Entity<BrickToBrick>()
                .HasOne(pt => pt.Brick)
                .WithMany(p => p.Children)
                .HasForeignKey(pt => pt.BrickId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BrickToBrick>()
                .HasOne(pt => pt.Child)
                .WithMany(t => t.Parents)
                .HasForeignKey(pt => pt.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BrickToBrick>().HasKey(sc => new { sc.BrickId, sc.ChildId });

        }
    }
}
