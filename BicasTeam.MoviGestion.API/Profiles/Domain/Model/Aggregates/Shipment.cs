namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;

public class Shipment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Destiny { get; set; }
    public string Description { get; set; }
    public DateTimeInfo DateTime { get; set; }
    public string Status { get; set; }
}

public class DateTimeInfo
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
}