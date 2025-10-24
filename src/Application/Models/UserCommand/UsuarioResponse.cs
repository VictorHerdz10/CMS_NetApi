namespace CMS_NetApi.Application.Models.UserCommand;


public record UsuarioResponse(
    string Id,
    string Nombre,
    string Email,
    string TipoUsuario,
    string Telefono
);