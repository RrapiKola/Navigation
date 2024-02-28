using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Interfaces;
using api.Mappers;
using api.Migrations;
using api.Models;
using api.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Journey = api.Models.Journey;

namespace api.Repository
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly ApplicationDbContext context;
        public JourneyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Journey> Add(CreateJourneyDto dto)
        {
            var journey = dto.MapToModel();

            await context.Journeys.AddAsync(journey);
            await context.SaveChangesAsync();

            return journey;
        }

        public async Task<Journey?> Delete(int journeyId)
        {
            var journeyModel = await context.Journeys.FirstOrDefaultAsync(j => j.Id == journeyId);
            if (journeyModel == null)
            {
                return null;
            }

            context.Journeys.Remove(journeyModel);
            await context.SaveChangesAsync();
            return journeyModel;
        }

        public async Task<Journey?> FindById(int journeyId)
        {
            return await context.Journeys.FirstOrDefaultAsync(j => j.Id == journeyId);
        }


        public async Task<List<Journey>> FindAll(QueryObject query)
        {
            var queryable = context.Journeys.AsQueryable();


            if (!string.IsNullOrEmpty(query.UserId))
            {
                queryable = queryable.Where(j => j.AppUserJourneys.Any(aj => aj.AppUserId == query.UserId));
            }

            if (query.StartTime.HasValue)
            {
                queryable = queryable.Where(j => j.StartTime == query.StartTime.Value);
            }


            if (query.ArrivalTime.HasValue)
            {
                queryable = queryable.Where(j => j.ArrivalTime == query.ArrivalTime.Value);
            }

            if (query.TransportationType.HasValue)
            {
                queryable = queryable.Where(j => j.TransportationType == query.TransportationType.Value);
            }

            return await queryable.ToListAsync();

        }

        public async Task<double> MonthlyRouteDistance()
        {
            DateTime currentUtcDateTime = DateTime.UtcNow;
            int currentMonth = currentUtcDateTime.Month;

            DateTime startOfMonth = new DateTime(currentUtcDateTime.Year, currentMonth, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1);

            var totalMonthlyDistance = await context.Journeys
                .Where(j => j.StartTime >= startOfMonth && j.ArrivalTime <= endOfMonth)
                .SumAsync(j => j.RouteDistance);

            return totalMonthlyDistance;
        }

        public async Task<Journey?> UpdateDailyAchievement(AppUser appUser, Journey userJourney)
        {
            var userJourneyList = await context.AppUserJourneys
                .Where(u => u.AppUserId == appUser.Id)
                .Select(j => new
                {
                    j.Journey.StartTime,
                    j.Journey.RouteDistance,
                })
                .ToListAsync();

            var currentJourneyDate = userJourney.StartTime.Date;
            var routeDistanceSumOfDay = userJourneyList
                .Where(j => j.StartTime.Date == currentJourneyDate)
                .Sum(j => j.RouteDistance);

            if (routeDistanceSumOfDay >= 20)
            {
                userJourney.DailyAchievement = true;
                await context.SaveChangesAsync();
            }

            return userJourney;
        }

    }
}