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
    public class MaterialsControllerTests
    {
        private MaterialsController _controller;
        private Mock<IMaterialRepository> _serviceMock;
        private Mock<ILogger<MaterialsController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IMaterialRepository>();
            _loggerMock = new Mock<ILogger<MaterialsController>>();
            _controller = new MaterialsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task DeleteMaterial_ReturnsOkResult()
        {
            //Act
            int id = 2;
            Material material = new Material() { ItemID = 1, ItemName = "Material 1" };

            _serviceMock.Setup(service => service.GetMaterialAsync(id)).ReturnsAsync(material);
            _serviceMock.Setup(service => service.DeleteMaterialAsync(material)).Returns(Task.CompletedTask);

            // Action

           var result = await _controller.DeleteMaterial(id);

            // Assert

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteMaterial_ReturnsNotFoundResult()
        {
            //Act
            int id = 20;
            Material material = null;

            _serviceMock.Setup(service => service.GetMaterialAsync(id)).ReturnsAsync(material);

            // Action
            var result = await _controller.DeleteMaterial(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

    }
}
