using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;


namespace ACME.LearningCenterPlatform.API.profiles_managment.Domain.Repositories;


public interface IShipmentRepository
{
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<Shipment?> GetByIdAsync(int id);
    Task AddAsync(Shipment shipment);

   
}