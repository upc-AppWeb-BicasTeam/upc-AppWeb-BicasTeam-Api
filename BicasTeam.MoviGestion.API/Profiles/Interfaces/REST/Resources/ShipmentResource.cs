namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

public record ShipmentResource (int UserId, string Destiny, string Description, DateTimeinfo DateTime, string Status);

public record DateTimeinfo(DateTime Date, TimeSpan Time);