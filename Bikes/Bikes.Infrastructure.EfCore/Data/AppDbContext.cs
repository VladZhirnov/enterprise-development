using Bikes.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Infrastructure.EfCore.Data;

/// <summary>
/// Database context for bike rental system
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Bike models
    /// </summary>
    public DbSet<BikeModel> BikeModels { get; set; }

    /// <summary>
    /// Bikes
    /// </summary>
    public DbSet<Bike> Bikes { get; set; }

    /// <summary>
    /// Clients
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Rentals
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BikeModel>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Type).IsRequired();
            entity.Property(x => x.WheelSize).IsRequired();
            entity.Property(x => x.MaxWeight).IsRequired();
            entity.Property(x => x.Weight).IsRequired();
            entity.Property(x => x.BrakeType).IsRequired();
            entity.Property(x => x.ModelYear).IsRequired();
            entity.Property(x => x.PricePerHour).HasPrecision(18, 2).IsRequired();

            entity.HasData(DataSeeder.BikeModels);
        });

        modelBuilder.Entity<Bike>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SerialNumber).IsRequired();
            entity.Property(x => x.Color).IsRequired();

            entity.HasIndex(x => x.SerialNumber).IsUnique();

            entity
                .HasOne(x => x.Model)
                .WithMany(x => x.Bikes)
                .HasForeignKey(x => x.ModelId)
                .IsRequired();

            entity.HasData(DataSeeder.Bikes);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.LastName).IsRequired();
            entity.Property(x => x.FirstName).IsRequired();
            entity.Property(x => x.MiddleName).IsRequired();
            entity.Property(x => x.Phone).IsRequired();

            entity.HasData(DataSeeder.Clients);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.StartTime).IsRequired();
            entity.Property(x => x.DurationHours).IsRequired();

            entity
                .HasOne(x => x.Bike)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.BikeId)
                .IsRequired();

            entity
                .HasOne(x => x.Client)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();

            entity.HasData(DataSeeder.Rentals);
        });
    }
}