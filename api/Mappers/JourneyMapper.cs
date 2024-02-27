using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account.Journey;
using api.Dtos.Journey;
using api.Models;



namespace api.Mappers
{
    public static class JourneyMapper
    {

        public static Journey MapToModel(this CreateJourneyDto dto)
        {
            return new Journey
            {
                StartTime = dto.StartTime,
                StartLocation = dto.StartLocation,
                ArrivalLocation = dto.ArrivalLocation,
                ArrivalTime = dto.ArrivalTime,
                TransportationType = dto.TransportationType,
                RouteDistance = dto.RouteDistance,

            };
        }


        public static JourneyDto MapToResponse(this Journey journey) {
            return new JourneyDto {
                Id = journey.Id,
                StartTime= journey.StartTime,
                StartLocation = journey.StartLocation,
                ArrivalTime = journey.ArrivalTime,
                ArrivalLocation = journey.ArrivalLocation,
                RouteDistance = journey.RouteDistance,
                TransportationType = journey.TransportationType,
            };
        }

    }
}