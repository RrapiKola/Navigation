using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAppUserJourneyRepository
    {
        Task<List<Journey>> FindUserJourneys(AppUser appUser);

        Task<AppUserJourney> Add(AppUserJourney appUserJourney);

        Task<AppUserJourney?> DeleteUserJourney(AppUser appUser, int journeyId);
    }
}