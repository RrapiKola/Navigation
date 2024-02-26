using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/journey")]
    public class JourneyController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public JourneyController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
    }
}