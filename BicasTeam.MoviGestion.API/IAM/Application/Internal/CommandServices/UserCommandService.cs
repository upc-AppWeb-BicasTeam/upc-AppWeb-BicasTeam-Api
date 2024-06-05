using BicasTeam.MoviGestion.API.IAM.Application.Internal.OutboundServices;
using BicasTeam.MoviGestion.API.IAM.Application.Internal.OutboundServices;
using BicasTeam.MoviGestion.API.IAM.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.IAM.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.IAM.Domain.Repositories;
using BicasTeam.MoviGestion.API.IAM.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

namespace BicasTeam.MoviGestion.API.IAM.Application.Internal.CommandServices;

/**
 * User command service.
 * <summary>
 *    This class is responsible for handling user commands.
 * </summary>
 */
public class UserCommandService(IUserRepository userRepository, ITokenService tokenService, IHashingService hashingService, IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists.");
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        } catch (Exception e)
        {
            throw new Exception("An error occurred while trying to sign up the user.", e);
        }
    }

    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password.");
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}