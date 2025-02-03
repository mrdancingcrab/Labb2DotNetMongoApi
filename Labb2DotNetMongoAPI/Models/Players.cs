using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Labb2DotNetMongoAPI.Models

{
    public class Players
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public string Club { get; set; }
    }
}