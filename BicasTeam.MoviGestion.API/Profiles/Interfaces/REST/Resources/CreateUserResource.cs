using BicasTeam.MoviGestion.API.Profiles.Domain.Model.ValueObjects;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

public record CreateUserResource(string Name, string LastName, string Email, string Password, EUserType Type);