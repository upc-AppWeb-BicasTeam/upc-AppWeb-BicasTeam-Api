using BicasTeam.MoviGestion.API.IAM.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    } 
}