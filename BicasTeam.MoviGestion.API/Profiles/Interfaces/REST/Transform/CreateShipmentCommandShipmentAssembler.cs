using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;

public static class CreateShipmentCommandShipmentAssembler
{
    public static CreateShipmentCommand ToCommandFromResource(ShipmentResource resource)
    {
        return new CreateShipmentCommand(
            resource.UserId,
            resource.Destiny,
            resource.Description,
            new DateTimeInfo(resource.DateTime.Date, resource.DateTime.Time),
            resource.Status
        );
    }
}
