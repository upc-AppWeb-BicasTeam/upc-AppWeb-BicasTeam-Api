using BicasTeam.MoviGestion.API.IAM.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    } 
}