using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Journey?> FindById(int journeyId)
        {
            return await context.Journeys.FirstOrDefaultAsync(j=>j.Id==journeyId);
        }

        
    }
}