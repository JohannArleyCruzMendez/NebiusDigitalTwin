using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using NebiusDigitalTwin.Domain.Entities;
using NebiusDigitalTwin.Infrastructure.Settings;

namespace NebiusDigitalTwin.Infrastructure.Data
{
    public class MongoDbContext
    {
        public IMongoCollection<Nodo> Nodos { get; }

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            ConfigurarMapeoBson();

            Nodos = database.GetCollection<Nodo>(settings.Value.CollectionName);
        }

        private void ConfigurarMapeoBson()
        {
            // Evita registrar el mapa dos veces si el contexto se reinicia
            if (!BsonClassMap.IsClassMapRegistered(typeof(Nodo)))
            {
                BsonClassMap.RegisterClassMap<Nodo>(cm =>
                {
                    cm.AutoMap();
                    // Mapea la propiedad Id de C# al campo _id de MongoDB generado automáticamente
                    cm.MapIdMember(n => n.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance)
                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
            }
        }
    }
}

