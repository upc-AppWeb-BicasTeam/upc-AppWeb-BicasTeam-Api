using BicasTeam.MoviGestion.API.Profiles.Domain.Model.ValueObjects;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

public record CreateUserCommand(string Name, string LastName, string Email, 
    string Password, EUserType Type);