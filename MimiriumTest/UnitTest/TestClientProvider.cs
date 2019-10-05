using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MimiriumTest;
using System.Net.Http;

namespace UnitTest
{
	public class TestClientProvider
	{
		public HttpClient Client { get; private set; }
		public TestClientProvider()
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json");

			var webBuilder = new WebHostBuilder()
				.UseConfiguration(builder.Build())
				.UseStartup<Startup>();

			var server = new TestServer(webBuilder);

			Client = server.CreateClient();
		}
	}
}
