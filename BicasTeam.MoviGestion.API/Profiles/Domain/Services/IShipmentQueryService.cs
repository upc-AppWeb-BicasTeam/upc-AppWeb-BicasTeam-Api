using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Services;

public interface IShipmentQueryService
{
    Task<IEnumerable<Shipment>> Handle(GetAllShipmentQuery query);
    
    
}