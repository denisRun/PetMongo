using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PetMongo.Enumerations;

namespace PetMongo.Models;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("password")]
    public string Password { get; set; }
    [BsonElement("full_name")]
    public string FullName { get; set; }
    [BsonElement("role")]
    public UserType Role { get; set; }
}