using CMS_NetApi.Application.Features.Auth.Command;
using CMS_NetApi.Application.Features.Users.Query;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Presentation.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS_NetApi.Presentation.Controllers;

[ApiController]
[Route("usuario")]
public class UsuarioController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Obtiene un usuario por email (via Application)
    /// </summary>
    [HttpGet("{email}")]
    public async Task<ActionResult<UsuarioResponse>> GetByEmail(
        [FromRoute] string email,
        CancellationToken ct)
    {
        var query = new ObtenerUsuarioPorEmailQuery(email);
        var response = await mediator.Send(query, ct);

        return response is null ? NotFound("Usuario no encontrado") : Ok(response);
    }


    /// <summary>
    /// ➕ COMMAND: Registrar nuevo usuario
    /// </summary>
    [HttpPost]
   [HttpPost]
public async Task<IActionResult> Registrar(
    [FromBody] UsuarioRequest dto, // <-- recibes solo el DTO
    CancellationToken ct)
{
    var command = new RegistrarUsuarioCommand(dto);
    var response = await mediator.Send(command, ct);
    return StatusCode(201, response);
}
}