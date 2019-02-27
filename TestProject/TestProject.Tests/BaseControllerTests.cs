using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TestProject.Services;

namespace TestProject.Tests
{
    public abstract class BaseControllerTests
    {
        protected HttpClient HttpClientForTest { get; set; }
        protected Mock<ICarService> CarServiceMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            CarServiceMock = new Mock<ICarService>(MockBehavior.Strict);

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .ConfigureTestServices(services =>
                {
                    services.AddTransient(provider => CarServiceMock.Object);
                })
                .UseStartup<Startup>();

            TestServer testServer = new TestServer(webHostBuilder);
            HttpClientForTest = testServer.CreateClient();
        }
    }
}
