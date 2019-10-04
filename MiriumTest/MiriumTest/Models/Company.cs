using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MiriumTest.Models
{
	public class Company
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
		public string Id { get; set; }
		public string Name { get; set; }
		public string Vat { get; set; }
	}
}
