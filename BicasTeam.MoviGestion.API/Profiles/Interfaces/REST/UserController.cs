using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST;

[ApiController]
[Route("/[controller]")]
public class UserController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService
): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserResource resource)
    {
        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(createUserCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetUserById), new { id = result.Id}, UserResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var result = await userQueryService.Handle(getUserByIdQuery);
        if (result is null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}