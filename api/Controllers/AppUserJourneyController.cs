using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/AppUserJourney")]
    public class AppUserJourneyController: ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IJourneyRepository journeyRepository;
        private readonly IAppUserJourneyRepository appUserJourneyRepository;

        public AppUserJourneyController(ApplicationDbContext context,UserManager<AppUser> userManager,IJourneyRepository journeyRepository,IAppUserJourneyRepository appUserJourneyRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.journeyRepository = journeyRepository;
            this.appUserJourneyRepository = appUserJourneyRepository;
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindUserJourneys(){
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            var userJourney = await appUserJourneyRepository.FindUserJourneys(appUser);
            return Ok(userJourney);

        }
        
    }
}