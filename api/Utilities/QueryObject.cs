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

        public TransportationType? TransportationType { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid StartTime format")]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid ArrivalTime format")]
        public DateTime? ArrivalTime { get; set; }

        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}