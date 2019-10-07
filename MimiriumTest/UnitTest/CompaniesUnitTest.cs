using System;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using MimiriumTest.Models;

namespace UnitTest
{
	public class CompaniesUnitTest
	{
		private HttpClient client;
		public CompaniesUnitTest()
		{
			client = new TestClientProvider().Client;
		}

		[Fact]
		public async Task Test_Get()
		{
			var response = await client.GetAsync("/api/companies");

			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
		
		[Fact]
		public async Task Test_Get_Name()
		{
			var response = await client.GetAsync("/api/companies?name=*");

			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Test_Get_ID()
		{
			string stringPayload = "{\"name\":\"MyCompany4\", " +
									"\"vat\":\"321434\" " +
									"}";
			var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/api/companies", httpContent);
			response.EnsureSuccessStatusCode();
			string companyJson = await response.Content.ReadAsStringAsync();
			Company company = JsonConvert.DeserializeObject<Company>(companyJson);

			response = await client.GetAsync("/api/companies/" + company.Id);

			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Test_Post()
		{
			string stringPayload = "{\"name\":\"MyCompany4\", " +
									"\"vat\":\"321434\" " +
									"}";
			var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/api/companies", httpContent);
			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async Task Test_Put()
		{
			string stringPayload = "{\"name\":\"MyCompany4\", " +
						"\"vat\":\"321434\" " +
						"}";
			var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/api/companies", httpContent);
			response.EnsureSuccessStatusCode();
			string companyJson = await response.Content.ReadAsStringAsync();
			Company company = JsonConvert.DeserializeObject<Company>(companyJson);

			stringPayload = "{\"_id\":" + company.Id + "," +
									"\"name\":\"MyCompany5\", " +
									"\"vat\":\"321411\" " +
									"}";

			response = await client.PutAsync("/api/companies/" + company.Id, httpContent);
			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

	}
}
