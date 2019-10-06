using Newtonsoft.Json;
namespace MobileApp.Models
{
	class Company
	{
		public long Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("vat")]
		public string Vat { get; set; }
	}
}