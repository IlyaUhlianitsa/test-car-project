using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TestProject.Common;
using TestProject.Services.Models;

namespace TestProject.Tests
{
    public class CarControllerTests : BaseControllerTests
    {
        private const int CarId = 123;

        [Test]
        public async Task GetCar_Ok()
        {
            var car = new Car();

            CarServiceMock
                .Setup(x => x.Get(CarId))
                .ReturnsAsync(car)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Get, $"car/{CarId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            string content = await response.Content.ReadAsStringAsync();
            var actualCar = JsonConvert.DeserializeObject<Car>(content);
            actualCar.Should().BeEquivalentTo(car);
            CarServiceMock.VerifyAll();
        }

        [Test]
        public async Task GetCar_NotFound()
        {
            CarServiceMock
                .Setup(x => x.Get(CarId))
                .ReturnsAsync((Car)null)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Get, $"car/{CarId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            string content = await response.Content.ReadAsStringAsync();
            var actualCar = JsonConvert.DeserializeObject<Car>(content);
            actualCar.Should().BeNull();
            CarServiceMock.VerifyAll();
        }
        
        [Test]
        public async Task CreateCar_OK()
        {
            var car = new Car
            {
                Description = "description",
                Title = "title"
            };
            var expectedCar = new Car();
            CarServiceMock
                .Setup(x => x.Create(It.IsAny<Car>()))
                .Callback((Car inputCar) => inputCar.Should().BeEquivalentTo(inputCar))
                .ReturnsAsync(expectedCar)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Post, "car", JsonConvert.SerializeObject(car));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            string content = await response.Content.ReadAsStringAsync();
            var actualCar = JsonConvert.DeserializeObject<Car>(content);
            actualCar.Should().BeEquivalentTo(expectedCar);
            CarServiceMock.VerifyAll();
        }

        [Test]
        public async Task CreateCar_BadRequest()
        {
            var car = new Car
            {
                Description = "description",
                Title = "title"
            };
            CarServiceMock
                .Setup(x => x.Create(It.IsAny<Car>()))
                .Callback((Car inputCar) => inputCar.Should().BeEquivalentTo(inputCar))
                .ReturnsAsync((Car) null)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Post, "car", JsonConvert.SerializeObject(car));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            string content = await response.Content.ReadAsStringAsync();
            var actualCar = JsonConvert.DeserializeObject<Car>(content);
            actualCar.Should().BeNull();
            CarServiceMock.VerifyAll();
        }

        [TestCase(true, HttpStatusCode.OK, TestName = "UpdateCar_OK")]
        [TestCase(false, HttpStatusCode.NotFound, TestName = "UpdateCar_NotFound")]
        public async Task UpdateCar(bool updated, HttpStatusCode statusCode)
        {
            var car = new Car
            {
                Id = CarId,
                Description = "description",
                Title = "title"
            };
            CarServiceMock
                .Setup(x => x.Update(It.IsAny<Car>()))
                .Callback((Car inputCar) => inputCar.Should().BeEquivalentTo(car))
                .ReturnsAsync(updated)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Patch, "car", JsonConvert.SerializeObject(car));

            response.StatusCode.Should().Be(statusCode);
            CarServiceMock.VerifyAll();
        }
        
        [Test]
        public async Task UpdateCar_BadRequest()
        {
            var car = new Car
            {
                Id = CarId,
                Description = "description"
            };
            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Patch, "car", JsonConvert.SerializeObject(car));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            string content = await response.Content.ReadAsStringAsync();
            content.Should().Be("{\"Title\":[\"The Title field is required.\"]}");
            CarServiceMock.VerifyNoOtherCalls();
        }

        [TestCase(true, HttpStatusCode.NoContent, TestName = "DeleteCar_OK")]
        [TestCase(false, HttpStatusCode.NotFound, TestName = "DeleteCar_NotFound")]
        public async Task DeleteCar(bool deleted, HttpStatusCode statusCode)
        {
            CarServiceMock
                .Setup(x => x.Delete(CarId))
                .ReturnsAsync(deleted)
                .Verifiable();

            HttpResponseMessage response = await HttpClientForTest.SendRequest(HttpMethod.Delete, $"car/{CarId}");

            response.StatusCode.Should().Be(statusCode);
            CarServiceMock.VerifyAll();
        }
    }
}
