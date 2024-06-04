using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Vehicle Context
        
        builder.Entity<Vehicle>().HasKey(f => f.Id);
        builder.Entity<Vehicle>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(f => f.LicensePlate).IsRequired();
        builder.Entity<Vehicle>().Property(f => f.Model).IsRequired();
        builder.Entity<Vehicle>().Property(f => f.SerialNumber).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}