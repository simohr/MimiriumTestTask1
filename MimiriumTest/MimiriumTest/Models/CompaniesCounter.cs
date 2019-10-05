using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MimiriumTest.Models
{
	public class CompaniesCounter
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
		public string Id { get; set; }

		[BsonElement("counter")]
		public long Counter { get; set; }
	}
}
