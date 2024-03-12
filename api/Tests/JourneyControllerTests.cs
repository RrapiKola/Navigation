using api.Controllers;
using api.Dtos.Account.Journey;
using api.Interfaces;
using api.Models;
using api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api.tests
{
    public class JourneyControllerTests
    {
        private readonly Mock<IJourneyRepository> journeyRepositoryMock;
        private readonly JourneyController controller;

        public JourneyControllerTests()
        {
            this.journeyRepositoryMock = new Mock<IJourneyRepository>();
            this.controller = new JourneyController(journeyRepositoryMock.Object);
        }


        [Fact]
        public async Task FindAll_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange
            var query = new QueryObject();
            var journeyList = new List<Journey>();
            journeyRepositoryMock.Setup(repo => repo.FindAll(query)).ReturnsAsync(journeyList);

            // Act
            var result = await controller.FindAll(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JourneyDto>>(okResult.Value);
            // Further assertion if needed
            // Assert.Equal(journeyListDto, returnValue);
        }



        [Fact]
        public async Task FindAll_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.FindAll(new QueryObject());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task FindMonthlyDistance_ReturnsOkResult()
        {
            // Arrange
            double distance = 100;
            journeyRepositoryMock.Setup(repo => repo.MonthlyRouteDistance()).ReturnsAsync(distance);

            // Act
            var result = await controller.FindMonthlyDistance();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(distance, okResult.Value);
        }

    }
}