using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Interfaces
{
    public interface IApplicationDbContext
{
    DbSet<Journey> Journeys { get; set; }
    DbSet<AppUserJourney> AppUserJourneys { get; set; }


    
}
}