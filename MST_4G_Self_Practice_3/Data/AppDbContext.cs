using Microsoft.EntityFrameworkCore;
using MST_4G_Self_Practice_3.Models;

namespace MST_4G_Self_Practice_3.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<County> County { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<County>(entity =>
        {
           entity.ToTable("county");
           entity.HasKey(c => c.CountyId);
           entity.Property(c => c.CountyId).HasColumnName("county_id");
           entity.Property(c => c.CountyNo).HasColumnName("county_no");
           entity.Property(c => c.CountyName).HasColumnName("county_name");
        });
    }
}