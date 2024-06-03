using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public async Task<User?> Handle(CreateUserCommand command)
    {
        var user = new User(command);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return user;
    }
}