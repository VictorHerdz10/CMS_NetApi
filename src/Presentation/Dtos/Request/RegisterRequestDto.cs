
namespace CMS_NetApi.Presentation.Dtos.Request
{
    /// <summary>
    /// DTO para registro de nuevos usuarios
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        /// <example>Juan Pérez</example>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Email del usuario (debe ser único en el sistema)
        /// </summary>
        /// <example>usuario@ejemplo.com</example>
        /// 
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña del usuario (mínimo 8 caracteres)
        /// </summary>
        /// <example>MiContraseñaSegura123</example>
        /// 
        public string Password { get; set; } = null!;
    }
}