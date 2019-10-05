using System;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Text;

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
			var response = await client.GetAsync("/api/companies?name=Mimi");

			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Test_Get_ID()
		{
			var response = await client.GetAsync("/api/companies/12");

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
			string stringPayload = "{\"_id\":\"12\", " +
									"\"name\":\"Mimirium\", " +
									"\"vat\":\"321411\" " +
									"}";
			var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

			var response = await client.PutAsync("/api/companies/12", httpContent);
			response.EnsureSuccessStatusCode();

			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}

	}
}
