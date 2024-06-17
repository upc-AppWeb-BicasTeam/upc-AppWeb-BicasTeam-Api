using BicasTeam.MoviGestion.API.IAM.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.Username);
    }
}