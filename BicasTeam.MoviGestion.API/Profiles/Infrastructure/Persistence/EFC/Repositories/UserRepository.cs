using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace BicasTeam.MoviGestion.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}