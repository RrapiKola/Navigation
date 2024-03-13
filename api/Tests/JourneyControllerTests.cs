using api.Controllers;
using api.Dtos.Account.Journey;
using api.Interfaces;
using api.Mappers;
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
            var query = new QueryObject
            {
                TransportationType = TransportationType.Bicycle,
                StartTime = DateTime.Now.AddDays(-1),
                ArrivalTime = DateTime.Now,
                IsDecsending = true,
                PageNumber = 1,
                PageSize = 10
            };
            var journeyList = new List<Journey>
            {
                new Journey
                {
                    Id = 1,
                    StartTime = DateTime.Now,
                    StartLocation = "Location A",
                    ArrivalLocation = "Location B",
                    ArrivalTime = DateTime.Now.AddHours(1),
                    TransportationType = TransportationType.Bicycle,
                    RouteDistance = 15.0,
                    AppUserJourneys = new List<AppUserJourney>()
                },
                new Journey
                {
                    Id = 2,
                    StartTime = DateTime.Now.AddDays(1),
                    StartLocation = "Location C",
                    ArrivalLocation = "Location D",
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(2),
                    TransportationType = TransportationType.Bicycle,
                    RouteDistance = 5.0,
                    AppUserJourneys = new List<AppUserJourney>()
                }
            };

            journeyRepositoryMock.Setup(repo => repo.FindAll(query)).ReturnsAsync(journeyList);

            // Act
            var result = await controller.FindAll(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JourneyDto>>(okResult.Value).ToList();
            Assert.Equal(journeyList.Count, returnValue.Count);
            journeyRepositoryMock.Verify(repo => repo.FindAll(query), Times.Once);
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
