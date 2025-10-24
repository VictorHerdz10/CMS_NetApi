using CMS_NetApi.Application.Models.UserCommand;
using CMS_NetApi.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace CMS_NetApi.Application.Features.Users.Query;
public record ObtenerUsuarioPorEmailQuery(string Email) : IRequest<UsuarioResponse?>;

internal sealed class ObtenerUsuarioPorEmailQueryHandler(
    IUsuarioRepository repo,
    IMapper mapper) : IRequestHandler<ObtenerUsuarioPorEmailQuery, UsuarioResponse?>
{
    public async Task<UsuarioResponse?> Handle(
        ObtenerUsuarioPorEmailQuery query,
        CancellationToken ct)
    {
        var user = await repo.GetByEmailAsync(query.Email, ct);
        return user is null ? null : mapper.Map<UsuarioResponse>(user);
    }
}