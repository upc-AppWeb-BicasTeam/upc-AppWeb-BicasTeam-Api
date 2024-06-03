namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

public record CreateShipmentCommand(int UserId, string Destiny, string Description, DateTimeInfo DateTime, string Status);

public record DateTimeInfo(DateTime Date, TimeSpan Time);