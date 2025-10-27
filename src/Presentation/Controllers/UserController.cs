using AutoMapper;
using CMS_NetApi.Application.Features.Auth.Command;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Presentation.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMS_NetApi.Presentation.Controllers;

[ApiController]
[Route("auth")]
public class UserController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Registra un nuevo usuario en el sistema
    /// </summary>
    /// <remarks>
    /// Ejemplo de solicitud:
    /// 
    ///     POST /auth/register
    ///     {
    ///         "nombre": "Juan Pérez",
    ///         "email": "usuario@ejemplo.com",
    ///         "password": "MiContraseñaSegura123"
    ///     }
    /// 
    /// **Nota:** Los emails específicos se asignan automáticamente como administradores.
    /// </remarks>
    /// <param name="dto">Datos del usuario a registrar</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>Confirmación del registro exitoso</returns>
    /// <response code="201">Usuario registrado exitosamente</response>
    /// <response code="400">Error de validación - Datos de entrada inválidos</response>
    /// <response code="409">El email ya está registrado en el sistema</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(object), 201)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 409)]
    [ProducesResponseType(typeof(object), 500)]
    public async Task<IActionResult> Registrar(
        [FromBody] RegisterRequestDto dto,
        CancellationToken ct)
    {
        var command = new RegisterUserCommand(mapper.Map<UsuarioRequest>(dto));
        var response = await mediator.Send(command, ct);
        return StatusCode(201, new { message = response });
    }
}