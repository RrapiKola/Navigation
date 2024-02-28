using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Utilities
{
    public class QueryObject
    {
        
        public string UserId { get; set; } = string.Empty;

        // You can specify a range of valid values for TransportationType if needed
        public TransportationType? TransportationType { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid StartTime format")]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid ArrivalTime format")]
        public DateTime? ArrivalTime { get; set; }

    }
}