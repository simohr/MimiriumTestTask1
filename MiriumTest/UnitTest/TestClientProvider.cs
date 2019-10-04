using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MiriumTest;
using System.Net.Http;

namespace UnitTest
{
	public class TestClientProvider
	{
		public HttpClient Client { get; private set; }
		public TestClientProvider()
		{
			var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

			Client = server.CreateClient();
		}
	}
}
