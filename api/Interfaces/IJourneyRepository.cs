using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Models;
using api.Utilities;

namespace api.Interfaces
{
    public interface IJourneyRepository
    {
        Task<Journey?> FindById(int journeyId);

        Task<Journey> Add(CreateJourneyDto dto);

        Task<Journey?> Delete(int journeyId);
        Task<List<Journey>> FindAll(QueryObject query);
        Task<Double> MonthlyRouteDistance();

        Task<JourneyDto?> UpdateDailyAchievement(AppUser appUser, Journey journey);


    }
}