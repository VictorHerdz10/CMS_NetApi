using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using CMS_NetApi.Application.Exceptions;
using AutoMapper;

namespace CMS_NetApi.Application.Features.Auth.Query;

public record AuthenticateUserQuery : IRequest<UsuarioResponse>;

internal sealed class AuthenticateUserQueryHandler(
    IUserRepository repo,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : IRequestHandler<AuthenticateUserQuery, UsuarioResponse>
{
    public async Task<UsuarioResponse> Handle(
        AuthenticateUserQuery query,
        CancellationToken ct)
    {
        // Obtener el usuario actual desde el contexto HTTP
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedException("Usuario no autenticado");
        }

        // Obtener el ID del usuario desde el token
        var userIdClaim = user.FindFirst("sub") ?? user.FindFirst("id");
        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            throw new UnauthorizedException("Token inválido - no contiene información de usuario");
        }

        // Buscar el usuario en la base de datos
        var usuario = await repo.GetUserByIdAsync(userIdClaim.Value, ct) ?? throw new NotFoundException("Usuario no encontrado");
        // Mapear a la respuesta 
        var response = mapper.Map<UsuarioResponse>(usuario);
       
        return response;
    }
}