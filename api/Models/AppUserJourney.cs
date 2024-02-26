using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("AppUserJourneys")]
    public class AppUserJourney
    {

        // public int Id { get; set; }
        public string AppUserId { get; set; }
        public int JourneyId { get; set; }
        public AppUser AppUser { get; set; }
        public Journey Journey { get; set; }
    }

}