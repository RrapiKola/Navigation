using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Implementation;

namespace api.Repository
{
    public class AppUserJourneyRepository : IAppUserJourneyRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IJourneyRepository journeyRepository;

        public AppUserJourneyRepository(ApplicationDbContext context,IJourneyRepository journeyRepository )
        {
            this.journeyRepository = journeyRepository;
            this.context = context;
        }

       

        public async Task<List<Journey>> FindUserJourneys(AppUser appUser)
        {
            return await context.AppUserJourneys.Where(u=>u.AppUserId == appUser.Id).Select(journey=>new Journey
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


         public async Task<AppUserJourney> Add(AppUserJourney appUserJourney)
        {
            await context.AppUserJourneys.AddAsync(appUserJourney);
            await context.SaveChangesAsync();
            return appUserJourney;
        }



        public async Task<AppUserJourney?> DeleteUserJourney(AppUser appUser, int journeyId)
        {
            var appUserJourneyModel = await context.AppUserJourneys
            .FirstOrDefaultAsync(j=>j.AppUserId==appUser.Id && j.Journey.Id==journeyId);
            
            if(appUserJourneyModel==null){
                return null;
            }

            context.AppUserJourneys.Remove(appUserJourneyModel);
            await context.SaveChangesAsync();
            await journeyRepository.Delete(journeyId);

            return appUserJourneyModel;
        }
    }
}