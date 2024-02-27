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
            var vehicles = new List<Vehicle>();
            var vehicle = new Vehicle { VehicleId = 1, VehicleModel = "Car" };

            _serviceMock.Setup(service => service.GetAllVehicleAsync()).ReturnsAsync(vehicles); // Mock GetAllVehicleAsync
            _serviceMock.Setup(service => service.CreateVehicleAsync(vehicle)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostVehicle(vehicle);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreSame(vehicle, createdAtActionResult?.Value);


        }

          

        [Test]
        public async Task PostVehicle_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var vehicle = new Vehicle { VehicleId = 1, VehicleModel = "Car" };
            List<Vehicle> vehicles = null;
            _serviceMock.Setup(service => service.GetAllVehicleAsync()).ReturnsAsync(vehicles);
            _serviceMock.Setup(service => service.CreateVehicleAsync(vehicle)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.PostVehicle(vehicle);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.AreEqual(500, statusCodeResult?.StatusCode);
        }

    }
}
