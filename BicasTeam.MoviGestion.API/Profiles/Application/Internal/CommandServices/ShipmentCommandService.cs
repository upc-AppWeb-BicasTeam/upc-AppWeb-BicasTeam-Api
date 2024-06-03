using ACME.LearningCenterPlatform.API.profiles_managment.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

using AggregateDateTimeInfo = BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates.DateTimeInfo;


namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.CommandServices;

public class ShipmentCommandService : IShipmentCommandService
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShipmentCommandService(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Shipment?> CreateShipmentAsync(CreateShipmentCommand command)
    {
        // Aquí podrías incluir lógica de validación del comando
        var shipment = new Shipment
        {
            UserId = command.UserId,
            Destiny = command.Destiny,
            Description = command.Description,
            DateTime = new AggregateDateTimeInfo
            {
                Date = command.DateTime.Date,
                Time = command.DateTime.Time
            },
            Status = command.Status
        };

        try
        {
            await _shipmentRepository.AddAsync(shipment);
            await _unitOfWork.CompleteAsync();
            return shipment;
        }
        catch (Exception e)
        {
           
            return null;
        }
    }
}
