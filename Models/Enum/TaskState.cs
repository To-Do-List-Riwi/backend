using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models.Enum
{
    public enum TaskState
    {
        [BsonRepresentation(BsonType.String)]
        pendiente,
        [BsonRepresentation(BsonType.String)]
        Proceso,
        [BsonRepresentation(BsonType.String)]
        Completada,
        [BsonRepresentation(BsonType.String)]
        Eliminada
    }
}