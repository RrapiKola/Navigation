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
    [Authorize(Roles = "Admin")]
    public class JourneyController : ControllerBase
    {

        private readonly IJourneyRepository journeyRepository;

        public JourneyController(IJourneyRepository journeyRepository)
        {

            this.journeyRepository = journeyRepository;
        }

        
        [HttpGet]
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