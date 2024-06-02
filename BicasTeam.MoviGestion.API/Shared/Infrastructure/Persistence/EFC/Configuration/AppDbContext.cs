using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
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
        
        // Shipment Context
        builder.Entity<Shipment>().HasKey(f => f.Id);
        builder.Entity<Shipment>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shipment>().Property(f => f.UserId).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Destiny).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Description).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Status).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}