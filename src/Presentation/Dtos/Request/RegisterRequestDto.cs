namespace CMS_NetApi.Presentation.Dtos.Request
{
    public class RegisterRequestDto
    {
        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
