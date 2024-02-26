using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Journeys")]
    public class Journey
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string ArrivalLocation { get; set; } = string.Empty;
        public DateTime StartingTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TransportationType TransportationType { get; set; }
        public double RouteDistance { get; set; }
        public bool DailyAchievement { get; set; }

        public List<AppUserJourney> AppUserJourneys { get; set; }= new List<AppUserJourney>();

    }
}