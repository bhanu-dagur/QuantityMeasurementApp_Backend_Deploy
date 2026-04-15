using QuantityMeasurementAppModelLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementAppRepositoryLayer.Context;

public class AppDbContext : DbContext
{
    public DbSet<QuantityMeasurementHistoryEntity> QuantityMeasurementHistories{ get; set; }
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuantityMeasurementHistoryEntity>().ToTable("MeasurementHistory");
        modelBuilder.Entity<User>().ToTable("Users");

        base.OnModelCreating(modelBuilder);
    }
}
    