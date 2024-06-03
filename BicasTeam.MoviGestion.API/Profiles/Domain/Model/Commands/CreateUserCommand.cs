namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

public record CreateUserCommand(string Name, string LastName, string Email, 
    string Password, string Type);