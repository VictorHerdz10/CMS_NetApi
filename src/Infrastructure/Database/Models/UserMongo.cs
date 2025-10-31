using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CMS_NetApi.Infrastructure.Database.Models;

public class UserMongo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nombre { get; set; } = null!;
  
    public string Email { get; set; } = null!;

    
    public string Password { get; set; } = null!;
    public string? Telefono { get; set; }
    public string TipoUsuario { get; set; } = "Sin Asignar";
    public string? RelacionId { get; set; }
}