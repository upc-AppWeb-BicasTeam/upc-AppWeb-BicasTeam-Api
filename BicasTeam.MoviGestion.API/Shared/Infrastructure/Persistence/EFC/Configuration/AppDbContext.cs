using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
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
        
        // Category Context
        builder.Entity<Report>().HasKey(f => f.Id);
        builder.Entity<Report>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(f => f.Type).IsRequired();
        builder.Entity<Report>().Property(f => f.UserId).IsRequired();
        builder.Entity<Report>().Property(f => f.Description).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}