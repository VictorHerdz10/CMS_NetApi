namespace CMS_NetApi.Presentation.Dtos.Request
{
    /// <summary>
    /// DTO para inicio de sesión de usuarios
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Email del usuario registrado
        /// </summary>
        /// <example>usuario@ejemplo.com</example>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        /// <example>MiContraseñaSegura123</example>
        public string Password { get; set; } = null!;
    }
}