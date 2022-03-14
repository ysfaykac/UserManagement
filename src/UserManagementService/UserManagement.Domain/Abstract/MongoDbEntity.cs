using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagement.Domain.Abstract;

public abstract class MongoDbEntity : ISoftDeletedEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    [BsonElement(Order = 0)]
    public string Id { get; set;} = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.Boolean)]
    [BsonElement(Order = 100)]
    public bool IsDeleted { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

 
}