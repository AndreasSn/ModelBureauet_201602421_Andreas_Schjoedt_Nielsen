using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModeBureauDB.Models;

namespace ModeBureauDB.Models
{
    public class ModeBureauDBContext : DbContext
    {
        public ModeBureauDBContext (DbContextOptions<ModeBureauDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelIncomingTask>()
                .HasKey(k => new {k.IncomingTaskId, k.ModelId});
            modelBuilder.Entity<ModelIncomingTask>()
                .HasOne(k => k.Model)
                .WithMany(b => b.ModelIncomingTasks)
                .HasForeignKey(br => br.ModelId);
            modelBuilder.Entity<ModelIncomingTask>()
                .HasOne(br => br.IncomingTask)
                .WithMany(br => br.ModelIncomingTasks)
                .HasForeignKey(bc => bc.IncomingTaskId);
        }

        public DbSet<ModeBureauDB.Models.Model> Model { get; set; }

        public DbSet<ModeBureauDB.Models.IncomingTask> IncomingTask { get; set; }
        public DbSet<ModeBureauDB.Models.ModelIncomingTask> ModelIncomingTask { get; set; }
    }
}
