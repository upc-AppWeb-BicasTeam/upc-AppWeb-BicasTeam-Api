using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.ValueObjects;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;

public class User
{
    public int Id { get; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public EUserType Type { get; private set; }

    protected User()
    {
        Id = 0;
        Name = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Type = EUserType.businessman;
    }

    public User(CreateUserCommand command)
    {
        Name = command.Name;
        LastName = command.LastName;
        Email = command.Email;
        Password = command.Password;
        Type = command.Type;
    }
}