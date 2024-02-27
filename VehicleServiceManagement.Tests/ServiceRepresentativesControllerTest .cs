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
    public class ServiceRepresentativesControllerTests
    {
        private ServiceRepresentativesController _controller;
        private Mock<IServiceRepresentativeRepository> _serviceMock;
        private Mock<ILogger<ServiceRepresentativesController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IServiceRepresentativeRepository>();
            _loggerMock = new Mock<ILogger<ServiceRepresentativesController>>();
            _controller = new ServiceRepresentativesController(_serviceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task PutServiceRepresentative_ReturnsNoContentResult()
        {
            // Arrange
            var id = 1;
            var serviceRepresentative = new ServiceRepresentative { RepresentativeID = id,  FirstName = "Test" };

            _serviceMock.Setup(service => service.GetServiceRepresentativeAsync(id)).ReturnsAsync(serviceRepresentative);
            _serviceMock.Setup(service => service.UpdateServiceRepresentativeAsync(serviceRepresentative)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutServiceRepresentative(id, serviceRepresentative);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);

            
        }

    }
}
