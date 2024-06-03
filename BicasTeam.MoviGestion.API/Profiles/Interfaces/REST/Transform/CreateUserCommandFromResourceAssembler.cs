using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) => 
        new(resource.Name, resource.LastName, resource.Email, resource.Password, resource.Type);
}