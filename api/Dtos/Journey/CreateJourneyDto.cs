using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Journey
{
    public class CreateJourneyDto
    {
        public DateTime StartTime { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string ArrivalLocation { get; set; } = string.Empty;
        public DateTime ArrivalTime { get; set; }
        public TransportationType TransportationType { get; set; }
        public double RouteDistance { get; set; }
    }
}