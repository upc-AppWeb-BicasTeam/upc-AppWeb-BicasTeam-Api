using BicasTeam.MoviGestion.API.Profiles.Domain.Model.ValueObjects;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);