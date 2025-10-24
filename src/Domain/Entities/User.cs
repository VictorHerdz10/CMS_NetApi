namespace CMS_NetApi.Domain.Entities;

public class User
{
    public string Id { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Telefono { get; set; }
    public string TipoUsuario { get; set; } = "Sin Asignar";
    public string? RelacionId { get; set; }
}