using MongoDB.Driver;
using CMS_NetApi.Infrastructure.Database.Models;

namespace CMS_NetApi.Infrastructure.Database.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(string ConnectionString, string DatabaseName)
        {  
            var client = new MongoClient(ConnectionString);
            _database = client.GetDatabase(DatabaseName);

            try
            {
                // Intenta acceder al cluster para verificar la conexión
                client.Cluster.Description.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo conectar a MongoDB.", ex);
            }
        }

        // Colecciones existentes
        public IMongoCollection<UserMongo> Usuarios =>
          _database.GetCollection<UserMongo>("users");

    }
}
