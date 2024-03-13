using System.Security.Claims;
using api.Controllers;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api.tests
{
    public class AppUserJourneyControllerTests
    {
        private readonly Mock<UserManager<AppUser>> userManagerMock;
        private readonly Mock<IJourneyRepository> journeyRepositoryMock;
        private readonly Mock<IAppUserJourneyRepository> appUserJourneyRepositoryMock;
        private readonly AppUserJourneyController controller;

        // private IAsyncEnumerable<JourneyDto>? userJourneyListDto;

        public AppUserJourneyControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            journeyRepositoryMock = new Mock<IJourneyRepository>();
            appUserJourneyRepositoryMock = new Mock<IAppUserJourneyRepository>();
            controller = new AppUserJourneyController(null, userManagerMock.Object, journeyRepositoryMock.Object, appUserJourneyRepositoryMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "username") })) }
            };
        }

        [Fact]
        public async Task FindUserJourneys_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var appUser = new AppUser { UserName = "username", Email = "user@gmail.com", Id = "userId" };
            userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(appUser).Verifiable();
            var journeys = new List<Journey>
            {
                new Journey
                {
                    Id = 1,
                    StartTime = DateTime.Now,
                    StartLocation = "Location1",
                    ArrivalLocation = "Location2",
                    ArrivalTime = DateTime.Now.AddHours(1),
                    TransportationType = TransportationType.Bus,
                    RouteDistance = 10.5,
                    DailyAchievement = false,
                    AppUserJourneys = new List<AppUserJourney>()
                },
                new Journey
                {
                    Id = 2,
                    StartTime = DateTime.Now,
                    StartLocation = "Location3",
                    ArrivalLocation = "Location4",
                    ArrivalTime = DateTime.Now.AddHours(2),
                    TransportationType = TransportationType.Train,
                    RouteDistance = 20.5,
                    DailyAchievement = true,
                    AppUserJourneys = new List<AppUserJourney>()
                },
            };

            appUserJourneyRepositoryMock
                .Setup(repo => repo.FindUserJourneys(appUser))
                .ReturnsAsync(journeys);

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(
                            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
                            "username"
                        )
                    },
                    "TestAuthenticationType"
                )
            );

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.FindUserJourneys();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<JourneyDto>>(okResult.Value);
        }

        [Fact]
        public async Task AddUserJourney_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser)null);

            // Act
            var result = await controller.AddUserJourney(new CreateJourneyDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser)null);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
