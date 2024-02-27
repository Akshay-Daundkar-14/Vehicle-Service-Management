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
    public class CustomersControllerTests
    {
        private CustomersController _controller;
        private Mock<ICustomerRepository> _serviceMock;
        private Mock<ILogger<CustomersController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<ICustomerRepository>();
            _loggerMock = new Mock<ILogger<CustomersController>>();
            _controller = new CustomersController(_serviceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetCustomers_ReturnsOkResult()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "Customer1" },
                new Customer { CustomerId = 2, FirstName= "Customer2" }
            };

            _serviceMock.Setup(service => service.GetAllCustomerAsync()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<Customer>>(okResult?.Value);
            var returnedCustomers = okResult?.Value as IEnumerable<Customer>;

            Assert.IsNotNull(returnedCustomers);
            Assert.AreEqual(customers.Count, returnedCustomers.Count());

           
        }

        [Test]
        public async Task GetCustomers_ReturnsNotFoundResult()
        {
            // Arrange

            List<Customer> customers = null;

            _serviceMock.Setup(service => service.GetAllCustomerAsync()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            
        }

    }
}
