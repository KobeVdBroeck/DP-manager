using DP_manager_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DP_manager_API.Data;

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
            builder.Property(s => s.PlantCode).IsRequired();

            builder.HasOne(s => s.Plant)
                .WithMany()
                .HasForeignKey(s => s.PlantCode)
                .HasPrincipalKey(p => p.Code)
                .IsRequired();

            builder.Property(s => s.MediumId).IsRequired();
            builder.HasOne(m => m.Medium)
                .WithMany()
                .HasForeignKey(s => s.MediumId)
                .HasPrincipalKey(m => m.Id)
                .IsRequired();

            builder.ToTable("CurrentStock");
        });


        modelBuilder.Entity<ArchiveEntry>().Property(s => s.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ArchiveEntry>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.PlantCode).IsRequired();

            builder.HasOne(s => s.Plant)
                .WithMany()
                .HasForeignKey(s => s.PlantCode)
                .HasPrincipalKey(p => p.Code)
                .IsRequired();

            builder.Property(s => s.MediumId).IsRequired();
            builder.HasOne(m => m.Medium)
                .WithMany()
                .HasForeignKey(s => s.MediumId)
                .HasPrincipalKey(m => m.Id)
                .IsRequired();

            builder.ToTable("ArchivedStock");
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Plant> PlantEntries { get; set; }
    public DbSet<Medium> MediumEntries { get; set; }
    public DbSet<StockEntry> StockEntries { get; set; }
    public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
}
