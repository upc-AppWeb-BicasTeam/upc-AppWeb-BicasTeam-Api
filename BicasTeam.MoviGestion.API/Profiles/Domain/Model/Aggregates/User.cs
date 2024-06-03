using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;

public class User
{
    public int Id { get; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; private set; }

    protected User()
    {
        Id = 0;
        Name = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Type = string.Empty;
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