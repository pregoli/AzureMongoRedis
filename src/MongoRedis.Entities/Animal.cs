using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoRedis.Entities.Base;

namespace MongoRedis.Entities
{
    [BsonIgnoreExtraElements]
    public class Animal : Entity
    {
        public string Name { get; set; }
        [BsonRepresentation(BsonType.String)]
        public AnimalType Type { get; set; }
        public string Color { get; set; }
    }
}
