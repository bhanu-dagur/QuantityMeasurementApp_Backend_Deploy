using QuantityMeasurementAppModelLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace QuantityMeasurementAppRepositoryLayer.Context;

public class AppDbContext : DbContext
{
    public DbSet<QuantityMeasurementHistoryEntity> QuantityMeasurementHistories{ get; set; }
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // appsettings.json read karega
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuantityMeasurementHistoryEntity>().ToTable("MeasurementHistory");
        modelBuilder.Entity<User>().ToTable("Users");

        base.OnModelCreating(modelBuilder);
    }
}
    