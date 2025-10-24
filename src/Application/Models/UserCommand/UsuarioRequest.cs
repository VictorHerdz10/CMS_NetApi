namespace CMS_NetApi.Application.Models.UserCommand;

public record UsuarioRequest(
    string Nombre,
    string Email,
    string Password
);