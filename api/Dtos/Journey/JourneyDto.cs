using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Account.Journey
{
    public class JourneyDto
    {
        public int JourneyId { get; set; }
        public DateTime StartTime { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string ArrivalLocation { get; set; } = string.Empty;
        public DateTime ArrivalTime { get; set; }
        public TransportationType TransportationType { get; set; }
        public double RouteDistance { get; set; }
    }
}