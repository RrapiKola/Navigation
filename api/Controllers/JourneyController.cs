using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/journey")]
    public class JourneyController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IJourneyRepository journeyRepository;

        public JourneyController(ApplicationDbContext context,UserManager<AppUser> userManager,IJourneyRepository journeyRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.journeyRepository = journeyRepository;
        }


        
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindAll([FromQuery] QueryObject query) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var journeyList = await journeyRepository.FindAll(query);
            var journeyListDto = journeyList.Select(j=>j.MapToResponse());

            return Ok(journeyListDto);
        }

        [HttpGet("monthlyRoute")]
        public async Task<IActionResult> FindMonthlyDistance(){
            return Ok(await journeyRepository.MonthlyRouteDistance());
        }

    }
}