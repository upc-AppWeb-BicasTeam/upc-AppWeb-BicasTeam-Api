using ACME.LearningCenterPlatform.API.profiles_managment.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;

namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.QueryServices;

public class ShipmentQueryService(IShipmentRepository _shipmentRepository) : IShipmentQueryService
{

    public async Task<IEnumerable<Shipment>> Handle(GetAllShipmentQuery query) => await _shipmentRepository.GetAllAsync();
}

