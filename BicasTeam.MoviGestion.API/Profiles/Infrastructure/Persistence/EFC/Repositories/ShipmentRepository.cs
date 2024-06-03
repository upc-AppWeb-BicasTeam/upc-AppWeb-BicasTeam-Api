using ACME.LearningCenterPlatform.API.profiles_managment.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ShipmentRepository : BaseRepository<Shipment>, IShipmentRepository
{
    // Constructor de la clase ShipmentRepository
    public ShipmentRepository(AppDbContext context) : base(context)
    {
    }
    
    // Implementación del método GetAllAsync definido en IShipmentRepository
    public  async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await Context.Shipments.ToListAsync();
    }

    // Implementación del método GetByIdAsync definido en IShipmentRepository
    public  async Task<Shipment?> GetByIdAsync(int id)
    {
        return await Context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
    }

    // Implementación del método AddAsync definido en IShipmentRepository
    public  async Task AddAsync(Shipment shipment)
    {
        await Context.Shipments.AddAsync(shipment);
        await Context.SaveChangesAsync();
    }
}
