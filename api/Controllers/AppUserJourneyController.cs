using System.Transactions;
using api.Data;
using api.Dtos.Journey;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/AppUserJourney")]
    public class AppUserJourneyController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IJourneyRepository journeyRepository;
        private readonly IAppUserJourneyRepository appUserJourneyRepository;

        public AppUserJourneyController(ApplicationDbContext context, UserManager<AppUser> userManager, IJourneyRepository journeyRepository, IAppUserJourneyRepository appUserJourneyRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.journeyRepository = journeyRepository;
            this.appUserJourneyRepository = appUserJourneyRepository;
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindUserJourneys()
        {
            var username = User.GetUsername();
            if (username == null)
            {

                return BadRequest("Username not found in claims");
            }
            var appUser = await userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            var userJourneyList = await appUserJourneyRepository.FindUserJourneys(appUser);

            var userJourneyListDto = userJourneyList.Select(JourneyMapper.MapToResponse).ToList();
            return Ok(userJourneyListDto);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserJourney(CreateJourneyDto dto)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return BadRequest("User not found");
            }

            var journey = await journeyRepository.Add(dto);

            if (journey == null)
            {
                return BadRequest("Failed to create journey");
            }


            var existingJourney = await journeyRepository.FindById(journey.Id);

            if (existingJourney == null)
            {
                return BadRequest("Journey not found");
            }

            var userJourneyList = await appUserJourneyRepository.FindUserJourneys(appUser);

            if (userJourneyList.Any(j => j.Id == existingJourney.Id))
            {
                return BadRequest("Journey already exists; cannot add the same journey");
            }

            var appUserJourneyModel = new AppUserJourney
            {
                AppUserId = appUser.Id,
                JourneyId = journey.Id
            };

            await appUserJourneyRepository.Add(appUserJourneyModel);

            if (appUserJourneyModel == null)
            {
                return StatusCode(500, "Could not create user journey");
            }
            else
            {
                var updatedJourneyDto = await journeyRepository.UpdateDailyAchievement(appUser, journey);
                return Created(nameof(AddUserJourney), updatedJourneyDto);
            }
        }
    }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int journeyId)
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            var userJourneyList = await appUserJourneyRepository.FindUserJourneys(appUser);
            var filteredJourney = userJourneyList.Where(j => j.Id == journeyId).ToList();

            if (filteredJourney.Count() == 1)
            {
                await appUserJourneyRepository.DeleteUserJourney(appUser, journeyId);
            }
            else
            {
                return BadRequest("Journey is not in your journeys list!");
            }

            return Ok();
        }

    }
}
