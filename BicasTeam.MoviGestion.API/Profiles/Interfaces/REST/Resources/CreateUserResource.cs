namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

public record CreateUserResource(string Name, string LastName, string Email, string Password, string Type);