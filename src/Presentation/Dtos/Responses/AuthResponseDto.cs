namespace CMS_NetApi.Presentation.Dtos.Responses
{
    /// <summary>
    /// DTO para respuesta de autenticación
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Token JWT generado para el usuario
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        public string Token { get; set; } = null!;
    }
}