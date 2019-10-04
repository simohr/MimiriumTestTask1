using System;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Threading;

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
	}
}
