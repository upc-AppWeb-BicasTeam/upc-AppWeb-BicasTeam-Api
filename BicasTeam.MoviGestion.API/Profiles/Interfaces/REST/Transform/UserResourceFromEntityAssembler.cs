using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;

public class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity) => new UserResource(entity.Id, entity.Name,
        entity.LastName, entity.Email, entity.Password, entity.Type);
}