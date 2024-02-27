using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceManagement.API.Controllers;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.Tests
{
    [TestFixture]
    public class VehiclesControllerTests
    {
        private VehiclesController _controller;
        private Mock<IVehicleRepository> _serviceMock;
        private Mock<ILogger<VehiclesController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IVehicleRepository>();
            _loggerMock = new Mock<ILogger<VehiclesController>>();
            _controller = new VehiclesController(_serviceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task PostVehicle_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var vehicle = new Vehicle { VehicleId = 1, VehicleModel = "Car" };           
            _serviceMock.Setup(service => service.CreateVehicleAsync(vehicle)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostVehicle(vehicle);
            
            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreSame(vehicle, createdAtActionResult?.Value);


        }

          

    }
}
