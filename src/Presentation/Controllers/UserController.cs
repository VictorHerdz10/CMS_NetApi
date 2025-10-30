using AutoMapper;
using CMS_NetApi.Application.Features.Auth.Command;
using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Presentation.Dtos.Request;
using CMS_NetApi.Presentation.Dtos.Responses;
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
    ///     POST /api/auth/register
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
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar(
        [FromBody] RegisterRequestDto dto,
        CancellationToken ct)
    {
        var command = new RegisterUserCommand(mapper.Map<UsuarioRequest>(dto));
        var response = await mediator.Send(command, ct);
        return StatusCode(StatusCodes.Status201Created, new { message = response });
    }


    /// <summary>
    /// Inicia sesión de usuario en el sistema
    /// </summary>
    /// <remarks>
    /// Ejemplo de solicitud:
    /// 
    ///     POST /api/auth/login
    ///     {
    ///         "email": "usuario@ejemplo.com",
    ///         "password": "MiContraseñaSegura123"
    ///     }
    /// </remarks>
    /// <param name="dto">Credenciales de inicio de sesión</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Token JWT de autenticación</returns>
    /// <response code="200">Inicio de sesión exitoso</response>
    /// <response code="400">Error de validación - Datos de entrada inválidos</response>
    /// <response code="401">Credenciales incorrectas</response>
    /// <response code="404">Usuario no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto dto,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(mapper.Map<LoginRequest>(dto));
        var token = await mediator.Send(command, cancellationToken);

        return Ok(new AuthResponse { Token = token });
    }
}