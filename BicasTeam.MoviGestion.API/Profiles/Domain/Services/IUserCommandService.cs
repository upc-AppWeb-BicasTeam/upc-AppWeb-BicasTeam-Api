using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);
}