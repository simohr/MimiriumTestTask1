using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MimiriumTest.Models
{
	public class Company
	{
		[BsonId]
		[BsonIgnoreIfDefault]
		public long Id { get; set; }
		[BsonElement("name")]
		public string Name { get; set; }
		[BsonElement("vat")]
		public string Vat { get; set; }
	}
}
