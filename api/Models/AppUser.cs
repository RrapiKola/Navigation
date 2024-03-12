using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public List<AppUserJourney> AppUserJourneys { get; set; } = new List<AppUserJourney>();

    }
}