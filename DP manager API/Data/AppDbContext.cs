using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using DP_manager_API.Entities;

namespace DP_manager_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>(builder =>
            {
                builder.HasKey(p => p.Code);
                builder.ToTable("Plant");
            });

            modelBuilder.Entity<Medium>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Medium>(builder =>
            {
                builder.ToTable("Medium");
            });

            modelBuilder.Entity<StockEntry>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<StockEntry>(builder =>
            {
                builder.HasKey(s => s.Id);
                builder.ToTable("CurrentStock");
            });

            modelBuilder.Entity<ArchiveEntry>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ArchiveEntry>(builder =>
            {
                builder.HasKey(s => s.Id);
                builder.ToTable("ArchivedStock");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Plant> PlantEntries { get; set; }
        public DbSet<Medium> MediumEntries { get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
    }
}
