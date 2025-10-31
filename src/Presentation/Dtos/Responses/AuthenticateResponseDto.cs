namespace CMS_NetApi.Presentation.Dtos.Responses;

/// <summary>
/// Representa la respuesta con la información del usuario autenticado
/// </summary>
/// <remarks>
/// Este DTO se utiliza para devolver la información completa del usuario
/// después de validar su token de autenticación JWT.
/// 
/// Ejemplo de respuesta:
/// 
///     {
///         "id": "507f1f77bcf86cd799439011",
///         "nombre": "Juan Pérez",
///         "email": "usuario@ejemplo.com", 
///         "tipoUsuario": "Admin",
///         "telefono": "+1234567890"
///     }
/// </remarks>
/// <param name="Id">Identificador único del usuario en el sistema</param>
/// <param name="Nombre">Nombre completo del usuario</param>
/// <param name="Email">Dirección de correo electrónico registrada</param>
/// <param name="TipoUsuario">Rol o tipo de usuario en el sistema</param>
/// <param name="Telefono">Número de teléfono de contacto (opcional)</param>
public record AuthenticateResponseDto(
    string Id,
    string Nombre,
    string Email,
    string TipoUsuario,
    string Telefono
);