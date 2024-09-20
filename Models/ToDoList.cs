using backend.Models.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class ToDoList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? Date_created { get; set; }
        public DateTime? Assigned_date { get; set; }
        public DateTime? Date_finish { get; set; }
        public string? State { get; set; }
    }
}