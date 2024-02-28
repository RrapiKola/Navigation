using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Journey
{
    public class CreateJourneyDto
    {
        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "StartLocation is required")]
        public string StartLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "ArrivalLocation is required")]
        public string ArrivalLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "ArrivalTime is required")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "TransportationType is required")]
        public TransportationType TransportationType { get; set; }

        [Required(ErrorMessage = "RouteDistance is required")]
        [Range(0, double.MaxValue, ErrorMessage = "RouteDistance must be a non-negative number")]
        public double RouteDistance { get; set; }
    }
}