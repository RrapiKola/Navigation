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
    public class JourneyRepository : IJourneyRepository
    {
        private readonly ApplicationDbContext context;
        public JourneyRepository(ApplicationDbContext context)
        {
            this.context = context;
            
        }

        
    }
}