using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AppUserJourneyRepository : IAppUserJourneyRepository
    {
        private readonly ApplicationDbContext context;

        public AppUserJourneyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        
        public async Task<List<Journey>> FindUserJourneys(AppUser user)
        {
            return await context.AppUserJourneys.Where(u=>u.AppUserId == user.Id).Select(journey=>new Journey
            {
                Id=journey.JourneyId,
                StartTime = journey.Journey.StartTime,
                StartLocation = journey.Journey.StartLocation,
                ArrivalLocation=journey.Journey.ArrivalLocation,
                ArrivalTime = journey.Journey.ArrivalTime,
                TransportationType = journey.Journey.TransportationType,
                RouteDistance=journey.Journey.RouteDistance,
                DailyAchievement = journey.Journey.DailyAchievement

            }).ToListAsync();
        }
    }
}