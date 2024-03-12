using api.Interfaces;
using api.Mappers;
using api.Utilities;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> FindAll([FromQuery] QueryObject query)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var journeyList = await journeyRepository.FindAll(query);
            var journeyListDto = journeyList.Select(j => j.MapToResponse());

            return Ok(journeyListDto);
        }

        [HttpGet("totalMonthlyRoute")]
        public async Task<IActionResult> FindMonthlyDistance()
        {
            return Ok(await journeyRepository.MonthlyRouteDistance());
        }

    }
}