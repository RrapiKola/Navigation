using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var appUser = await userManager.FindByNameAsync(username);
            var userJourneyList = await appUserJourneyRepository.FindUserJourneys(appUser);
            return Ok(userJourneyList.Select(j => JourneyMapper.MapToResponse(j)));
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserJourney(CreateJourneyDto dto)
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);

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
                return Created();
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int journeyId){
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            var userJourneyList = await appUserJourneyRepository.FindUserJourneys(appUser);
            var filteredJourney = userJourneyList.Where(j=>j.Id==journeyId).ToList();

            if(filteredJourney.Count()==1){
                await appUserJourneyRepository.DeleteUserJourney(appUser,journeyId);
            }
            else
            {
                return BadRequest("Journey is not in your journeys list!");
            }

            return Ok();


        }











    }
}