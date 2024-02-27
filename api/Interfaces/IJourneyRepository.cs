using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Models;

namespace api.Interfaces
{
    public interface IJourneyRepository
    {
       Task<Journey?>FindById(int journeyId);

       Task<Journey>Add(CreateJourneyDto dto);
    }
}