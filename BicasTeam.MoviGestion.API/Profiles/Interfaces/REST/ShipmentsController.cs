using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST;




[ApiController]
[Route("api/[controller]")]
public class ShipmentsController(
    IShipmentCommandService shipmentCommandService,
    IShipmentQueryService shipmentQueryService) : ControllerBase
{
    

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all categories",
        Description = "Gets all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(200, "The categories were found", typeof(IEnumerable<ShipmentResource>))]
    public async Task<IActionResult> GetAllShipments()
    {
        var getAllShipmentsQuery = new GetAllShipmentQuery();
        var shipment = await shipmentQueryService.Handle(getAllShipmentsQuery);
        if (shipment == null) return NotFound();
        return Ok(shipment);
    }
    
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShipmentById(int id)
    {
        var getAllShipmentsQuery = new GetAllShipmentQuery();
        var shipment = await shipmentQueryService.Handle(getAllShipmentsQuery);
        if (shipment == null) return NotFound();
        return Ok(shipment);
    }

    
    
    [HttpPost]
    public async Task<IActionResult> CreateShipment([FromBody] ShipmentResource command)
    
    {
        
        var createShipmentCommand = CreateShipmentCommandShipmentAssembler.ToCommandFromResource(command);
        var result = await shipmentCommandService.CreateShipmentAsync(createShipmentCommand); 
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetShipmentById), new { id = result.Id }, result);
    }
    
    
    
    
}
