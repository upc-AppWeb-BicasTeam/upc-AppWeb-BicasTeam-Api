﻿using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;

namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public Task<Profile?> Handle(GetProfileByEmail query)
    {
        throw new NotImplementedException();
    }

    public Task<Profile?> Handle(GetProfileByEmailAndPassword query)
    {
        throw new NotImplementedException();
    }
}