using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;

public class Vehicle
{
    public int Id { get; set; }
    public string LicensePlate { get; private set; }
    public string Model {  get; private set; }
    public string SerialNumber {  get; private set; }

    protected Vehicle()
    {
        this.LicensePlate = string.Empty;
        this.Model = string.Empty;
        this.SerialNumber = string.Empty;
    }
    
    public Vehicle(CreateVehicleCommand command)
    {
        this.LicensePlate = command.LicensePlate;
        this.Model = command.Model;
        this.SerialNumber = command.SerialNumber;
    }
}